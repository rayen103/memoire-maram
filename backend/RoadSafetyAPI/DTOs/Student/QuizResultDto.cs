namespace RoadSafetyAPI.DTOs.Student;

public class QuizResultDto
{
    public int QuizId { get; set; }
    public string QuizTitle { get; set; } = string.Empty;
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public double Score { get; set; }
    public List<StudentAnswerDetailDto> Answers { get; set; } = new();
}

public class StudentAnswerDetailDto
{
    public string QuestionContent { get; set; } = string.Empty;
    public string SelectedAnswerContent { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public string CorrectAnswerContent { get; set; } = string.Empty;
}
