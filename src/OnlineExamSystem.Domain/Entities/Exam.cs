
using OnlineExamSystem.Domain.Identity;

namespace OnlineExamSystem.Domain.Entities;
public class Exam
{
    public int Id { get; set; }
    public required string ExamNo { get; set; }
    public required int QuestionsCount { get; set; }
    public DateTime CreatedDate { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
    public virtual ICollection<TakenExam> TakenExams { get; set; }

    public int InstructorId { get; set; }
    public ApplicationUser Instructor { get; set; }
    public virtual ICollection<Question>? Questions { get; set; }
    public virtual ICollection<ExamQuestion>? ExamQuestions { get; set; }
}
