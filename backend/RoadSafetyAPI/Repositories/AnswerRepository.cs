using Microsoft.EntityFrameworkCore;
using RoadSafetyAPI.Data;
using RoadSafetyAPI.Models;
using RoadSafetyAPI.Repositories.Interfaces;

namespace RoadSafetyAPI.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly AppDbContext _context;

    public AnswerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Answer>> GetByQuestionIdAsync(int questionId)
    {
        return await _context.Answers
            .Where(a => a.QuestionId == questionId)
            .ToListAsync();
    }

    public async Task<Answer?> GetByIdAsync(int id)
    {
        return await _context.Answers.FindAsync(id);
    }

    public async Task<Answer> CreateAsync(Answer answer)
    {
        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task UpdateAsync(Answer answer)
    {
        _context.Answers.Update(answer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var answer = await _context.Answers.FindAsync(id);
        if (answer != null)
        {
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
        }
    }
}
