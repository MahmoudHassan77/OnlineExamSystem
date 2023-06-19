
namespace OnlineExamSystem.Domain.Entities;
public class QuestionOptions
{
    public int Id { get; set; }
    public required string Option { get; set; }
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; }
}
