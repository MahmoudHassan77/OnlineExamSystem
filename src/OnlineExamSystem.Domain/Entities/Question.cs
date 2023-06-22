

namespace OnlineExamSystem.Domain.Entities;
public class Question
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public string? Details { get; set; }
    public required string Answer { get; set;}
    public required int Mark { get; set; }
    public required int Time { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
    public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
    public virtual ICollection<ExamQuestion>? ExamQuestions { get; set; }
    public virtual ICollection<TakenExamQuestion>? TakenExamQuestions { get; set; }

}
