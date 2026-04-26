using RoadSafetyAPI.DTOs.Answer;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;
using RoadSafetyAPI.Services.Interfaces;

namespace RoadSafetyAPI.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;

    public AnswerService(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<List<AnswerDto>> GetByQuestionIdAsync(int questionId)
    {
        var answers = await _answerRepository.GetByQuestionIdAsync(questionId);
        return answers.Select(MapToDto).ToList();
    }

    public async Task<AnswerDto?> GetByIdAsync(int id)
    {
        var answer = await _answerRepository.GetByIdAsync(id);
        return answer == null ? null : MapToDto(answer);
    }

    public async Task<AnswerDto> CreateAsync(CreateAnswerDto dto)
    {
        var answer = new Answer
        {
            QuestionId = dto.QuestionId,
            Content = dto.Content,
            IsCorrect = dto.IsCorrect
        };
        var created = await _answerRepository.CreateAsync(answer);
        return MapToDto(created);
    }

    public async Task<AnswerDto?> UpdateAsync(int id, UpdateAnswerDto dto)
    {
        var answer = await _answerRepository.GetByIdAsync(id);
        if (answer == null) return null;

        if (dto.Content != null) answer.Content = dto.Content;
        if (dto.IsCorrect.HasValue) answer.IsCorrect = dto.IsCorrect.Value;

        await _answerRepository.UpdateAsync(answer);
        return MapToDto(answer);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var answer = await _answerRepository.GetByIdAsync(id);
        if (answer == null) return false;

        await _answerRepository.DeleteAsync(id);
        return true;
    }

    private static AnswerDto MapToDto(Answer answer) => new AnswerDto
    {
        Id = answer.Id,
        QuestionId = answer.QuestionId,
        Content = answer.Content,
        IsCorrect = answer.IsCorrect
    };
}
