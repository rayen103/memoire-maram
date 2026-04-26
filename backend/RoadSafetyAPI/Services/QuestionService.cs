using RoadSafetyAPI.DTOs.Answer;
using RoadSafetyAPI.DTOs.Question;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<List<QuestionDto>> GetByQuizIdAsync(int quizId)
    {
        var questions = await _questionRepository.GetByQuizIdAsync(quizId);
        return questions.Select(MapToDto).ToList();
    }

    public async Task<QuestionDto?> GetByIdAsync(int id)
    {
        var question = await _questionRepository.GetByIdWithAnswersAsync(id);
        return question == null ? null : MapToDto(question);
    }

    public async Task<QuestionDto> CreateAsync(CreateQuestionDto dto)
    {
        var question = new Question
        {
            QuizId = dto.QuizId,
            Content = dto.Content
        };
        var created = await _questionRepository.CreateAsync(question);
        return MapToDto(created);
    }

    public async Task<QuestionDto?> UpdateAsync(int id, UpdateQuestionDto dto)
    {
        var question = await _questionRepository.GetByIdAsync(id);
        if (question == null) return null;

        if (dto.Content != null) question.Content = dto.Content;

        await _questionRepository.UpdateAsync(question);
        return MapToDto(question);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var question = await _questionRepository.GetByIdAsync(id);
        if (question == null) return false;

        await _questionRepository.DeleteAsync(id);
        return true;
    }

    private static QuestionDto MapToDto(Question question) => new QuestionDto
    {
        Id = question.Id,
        QuizId = question.QuizId,
        Content = question.Content,
        Answers = question.Answers?.Select(a => new AnswerDto
        {
            Id = a.Id,
            QuestionId = a.QuestionId,
            Content = a.Content,
            IsCorrect = a.IsCorrect
        }).ToList() ?? new List<AnswerDto>()
    };
}
