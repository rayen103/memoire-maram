using RoadSafetyAPI.DTOs.Common;
using RoadSafetyAPI.DTOs.Quiz;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;

    public QuizService(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<PagedResultDto<QuizDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var result = await _quizRepository.GetAllAsync(pageNumber, pageSize);
        return new PagedResultDto<QuizDto>
        {
            Items = result.Items.Select(MapToDto).ToList(),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }

    public async Task<QuizDto?> GetByIdAsync(int id)
    {
        var quiz = await _quizRepository.GetByIdWithQuestionsAsync(id);
        return quiz == null ? null : MapToDto(quiz);
    }

    public async Task<List<QuizDto>> GetByLevelAsync(int level)
    {
        var quizzes = await _quizRepository.GetByLevelAsync(level);
        return quizzes.Select(MapToDto).ToList();
    }

    public async Task<QuizDto> CreateAsync(CreateQuizDto dto)
    {
        var quiz = new Quiz
        {
            Title = dto.Title,
            Level = dto.Level
        };
        var created = await _quizRepository.CreateAsync(quiz);
        return MapToDto(created);
    }

    public async Task<QuizDto?> UpdateAsync(int id, UpdateQuizDto dto)
    {
        var quiz = await _quizRepository.GetByIdAsync(id);
        if (quiz == null) return null;

        if (dto.Title != null) quiz.Title = dto.Title;
        if (dto.Level.HasValue) quiz.Level = dto.Level.Value;

        await _quizRepository.UpdateAsync(quiz);
        return MapToDto(quiz);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var quiz = await _quizRepository.GetByIdAsync(id);
        if (quiz == null) return false;

        await _quizRepository.DeleteAsync(id);
        return true;
    }

    private static QuizDto MapToDto(Quiz quiz) => new QuizDto
    {
        Id = quiz.Id,
        Title = quiz.Title,
        Level = quiz.Level,
        QuestionCount = quiz.Questions?.Count ?? 0
    };
}
