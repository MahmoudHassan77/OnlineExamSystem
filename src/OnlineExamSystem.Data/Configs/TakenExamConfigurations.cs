using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExamSystem.Domain.Entities;

namespace OnlineExamSystem.Data.Configs;
public class TakenExamConfigurations : IEntityTypeConfiguration<TakenExam>
{
    public void Configure(EntityTypeBuilder<TakenExam> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate)
               .HasColumnType("datetime")
               .IsRequired();
        builder.Property(x => x.Mark)
               .IsRequired();
        builder.HasOne(x => x.Exam)
               .WithMany(x => x.TakenExams)
               .HasForeignKey(x => x.ExamId)
               .IsRequired();
        builder.HasMany(x => x.Students)
               .WithMany(x => x.TakenExams)
               .UsingEntity<StudentExam>();
        builder.HasMany(x => x.Questions)
               .WithMany(x => x.TakenExams)
               .UsingEntity<TakenExamQuestion>();
        builder.ToTable("TakenExams");
    }
}
