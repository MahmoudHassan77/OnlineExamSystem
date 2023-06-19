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
        builder.ToTable("StudentExams");
    }
}
