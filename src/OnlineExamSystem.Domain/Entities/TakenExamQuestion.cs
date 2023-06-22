namespace OnlineExamSystem.Domain.Entities;

public class TakenExamQuestion
{
    public int Id { get; set; }
    public int TakenExamId { get; set; }
    public TakenExam TakenExam { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    public int Mark { get; set; }
}