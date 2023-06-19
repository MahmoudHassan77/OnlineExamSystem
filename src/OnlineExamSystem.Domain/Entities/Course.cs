namespace OnlineExamSystem.Domain.Entities;
public class Course
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set;}
    public virtual ICollection<Exam>? Exams { get; set; }
    public virtual ICollection<Question>? Questions { get; set; }

}
