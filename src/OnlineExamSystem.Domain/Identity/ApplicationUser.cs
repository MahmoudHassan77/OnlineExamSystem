
using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Domain.Entities;

namespace OnlineExamSystem.Domain.Identity;
public class ApplicationUser : IdentityUser
{
    public required string Name { get; set; }
    public int Age { get; set; }
    public virtual ICollection<StudentExam>? StudentExams { get; set; }
    public virtual ICollection<Exam>? Exams { get; set; }
}