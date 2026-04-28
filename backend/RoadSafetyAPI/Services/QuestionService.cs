using RoadSafetyAPI.DTOs.Answer;
using RoadSafetyAPI.DTOs.Correction;
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
            Content = dto.Content,
            Type = dto.Type ?? string.Empty,
            Image = dto.Image ?? string.Empty,
            Explication = dto.Explication ?? string.Empty
        };
        var created = await _questionRepository.CreateAsync(question);
        return MapToDto(created);
    }

    public async Task<QuestionDto?> UpdateAsync(int id, UpdateQuestionDto dto)
    {
        var question = await _questionRepository.GetByIdAsync(id);
        if (question == null) return null;

        if (dto.Content != null) question.Content = dto.Content;
        if (dto.Type != null) question.Type = dto.Type;
        if (dto.Image != null) question.Image = dto.Image;
        if (dto.Explication != null) question.Explication = dto.Explication;

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
        Type = question.Type,
        Image = question.Image,
        Explication = question.Explication,
        Answers = question.Answers?.Select(a => new AnswerDto
        {
            Id = a.Id,
            QuestionId = a.QuestionId,
            Content = a.Content,
            IsCorrect = a.IsCorrect
        }).ToList() ?? new List<AnswerDto>(),
        Correction = question.Correction == null ? null : new CorrectionDto
        {
            Id = question.Correction.Id,
            QuestionId = question.Correction.QuestionId,
            Text = question.Correction.Text,
            Image = question.Correction.Image,
            Video = question.Correction.Video
        }
    };
}
