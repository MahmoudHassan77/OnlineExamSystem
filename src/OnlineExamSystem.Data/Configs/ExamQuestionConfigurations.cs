using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExamSystem.Domain.Entities;

namespace OnlineExamSystem.Data.Configs;

public class ExamQuestionConfigurations : IEntityTypeConfiguration<ExamQuestion>
{
    public void Configure(EntityTypeBuilder<ExamQuestion> builder)
    {
        builder.HasOne(x => x.Question)
               .WithMany(x => x.ExamQuestions)
               .HasForeignKey(x => x.QuestionId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Exam)
               .WithMany(x => x.ExamQuestions)
               .HasForeignKey(x => x.ExamId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("ExamQuestions");
    }
}
