using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Domain.Entities;
public class StudentExam
{
    public int Id { get; set; }
    public string StudentId { get; set; }
    public ApplicationUser Student { get; set; }
    public int TakenExamId { get; set; }
    public TakenExam TakenExam { get; set; }
    public DateTime CreatedDate { get; set; }
}
