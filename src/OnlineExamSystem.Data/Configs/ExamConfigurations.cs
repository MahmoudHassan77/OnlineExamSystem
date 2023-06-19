using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExamSystem.Domain.Entities;

namespace OnlineExamSystem.Data.Configs;
public class ExamConfigurations : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=> x.ExamNo)
               .IsRequired();
        builder.Property(x=> x.CreatedDate)
               .HasColumnName("datetime")
               .IsRequired();
        builder.Property(x=> x.QuestionsCount)
               .IsRequired();
        builder.HasOne(x => x.Instructor)
               .WithMany(x => x.Exams)
               .HasForeignKey(x => x.InstructorId)
               .IsRequired();
        builder.HasMany(x => x.Questions)
               .WithMany(x => x.Exams)
               .UsingEntity<ExamQuestion>();
        builder.ToTable("Exams");
    }
}
