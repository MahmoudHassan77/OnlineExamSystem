namespace OnlineExamSystem.Domain.Entities;

public class ExamQuestion
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public Exam Exam { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}