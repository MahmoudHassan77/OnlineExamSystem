using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExamSystem.Domain.Entities;

namespace OnlineExamSystem.Data.Configs;

public class TakenExamQuestionConfigurations : IEntityTypeConfiguration<TakenExamQuestion>
{
    public void Configure(EntityTypeBuilder<TakenExamQuestion> builder)
    {
        builder.HasOne(x => x.Question)
               .WithMany(x => x.TakenExamQuestions)
               .HasForeignKey(x => x.QuestionId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.TakenExam)
               .WithMany(x => x.TakenExamQuestions)
               .HasForeignKey(x => x.TakenExamId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("TakenExamQuestions");
    }
}