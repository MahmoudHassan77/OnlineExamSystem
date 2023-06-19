using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExamSystem.Domain.Entities;
namespace OnlineExamSystem.Data.Configs;
public class CourseConfigurations : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate)
               .HasColumnName("datetime")
               .IsRequired();
        builder.Property(x=> x.Name) 
               .IsRequired();
        builder.HasMany(x => x.Exams)
               .WithOne(x => x.Course)
               .HasForeignKey(x => x.CourseId)
               .IsRequired();
    }
}

