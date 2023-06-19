namespace OnlineExamSystem.Domain.Entities;
public class StudentExam
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int TakenExamId { get; set; }
    public DateTime CreatedDate { get; set; }
}
