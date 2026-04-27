using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RoadSafetyAPI.DTOs.Auth;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IParentRepository _parentRepository;
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostEnvironment;

    public AuthService(
        IUserRepository userRepository,
        IStudentRepository studentRepository,
        IParentRepository parentRepository,
        IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        _userRepository = userRepository;
        _studentRepository = studentRepository;
        _parentRepository = parentRepository;
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new InvalidOperationException("Email already in use.");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role.ToUpper()
        };

        await _userRepository.CreateAsync(user);

        if (user.Role == "STUDENT")
        {
            var studentProfile = new StudentProfile
            {
                UserId = user.Id,
                Points = 0,
                Level = 1
            };
            await _studentRepository.CreateAsync(studentProfile);
        }
        else if (user.Role == "PARENT")
        {
            await _parentRepository.CreateAsync(new ParentProfile
            {
                UserId = user.Id
            });
        }

        var token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
            UserId = user.Id
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
            UserId = user.Id
        };
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = JwtSecretKeyResolver.Resolve(_configuration, _hostEnvironment.IsDevelopment());
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var expiryDays = int.Parse(jwtSettings["ExpiryDays"] ?? "7");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(expiryDays),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
