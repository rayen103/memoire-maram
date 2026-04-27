using Microsoft.Extensions.Configuration;

namespace RoadSafetyAPI.Services;

public static class JwtSecretKeyResolver
{
    private const string DevelopmentFallbackSecret =
        "RoadSafetyAPI_Local_Development_Only_Secret_Key_2026_Replace_For_Real_Deployments!";

    public static string Resolve(IConfiguration configuration, bool isDevelopment)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? jwtSettings["SecretKey"];

        if (!string.IsNullOrWhiteSpace(secretKey) &&
            !secretKey.Contains("CHANGE_ME", StringComparison.OrdinalIgnoreCase))
        {
            return secretKey;
        }

        if (isDevelopment || IsDebugBuild())
        {
            return DevelopmentFallbackSecret;
        }

        throw new InvalidOperationException("JWT SecretKey not configured. Set JWT_SECRET_KEY environment variable.");
    }

    private static bool IsDebugBuild()
    {
#if DEBUG
        return true;
#else
        return false;
#endif
    }
}
