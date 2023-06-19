using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExamSystem.Domain.Entities;

namespace OnlineExamSystem.Data.Configs;

public class QuestionConfigurations : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=> x.Body).IsRequired();
        builder.Property(x=> x.Answer).IsRequired();
        builder.Property(x=> x.Mark).IsRequired();
        builder.Property(x=> x.Time).IsRequired();
        builder.HasOne(x => x.Course)
               .WithMany(x => x.Questions)
               .HasForeignKey(x => x.CourseId);

        builder.ToTable("Questions");
    }
}
