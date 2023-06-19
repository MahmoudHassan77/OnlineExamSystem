
namespace OnlineExamSystem.Domain.Entities;
public class TakenExam
{
    public int Id { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required int Mark { get; set; }
    public int ExamId { get; set; }
    public Exam Exam { get; set; }
    public virtual ICollection<ApplicationUser> Students { get; set; }
    public virtual ICollection<StudentExam>? StudentExams { get; set; }
    public virtual ICollection<Question>? Questions { get; set; }
    public virtual ICollection<TakenExamQuestion>? TakenExamQuestions { get; set; }
}
