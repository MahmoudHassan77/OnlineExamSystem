
using Microsoft.AspNetCore.Identity;

namespace OnlineExamSystem.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public required string Name { get; set; }
    public int Age { get; set; }
    public virtual ICollection<TakenExam>? TakenExams { get; set; }
    public virtual ICollection<Exam>? Exams { get; set; }
}
