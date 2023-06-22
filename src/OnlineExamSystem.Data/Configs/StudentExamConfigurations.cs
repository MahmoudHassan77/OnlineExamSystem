using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExamSystem.Domain.Entities;

namespace OnlineExamSystem.Data.Configs;
public class StudentExamConfigurations : IEntityTypeConfiguration<StudentExam>
{
    public void Configure(EntityTypeBuilder<StudentExam> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate)
               .HasColumnType("datetime")
               .IsRequired();
        builder.HasOne(x => x.Student)
               .WithMany(x => x.StudentExams)
               .HasForeignKey(x => x.StudentId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.TakenExam)
               .WithMany(x => x.StudentExams)
               .HasForeignKey(x => x.TakenExamId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("StudentExams");
    }
}
