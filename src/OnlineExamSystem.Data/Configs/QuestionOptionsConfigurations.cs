using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExamSystem.Domain.Entities;

namespace OnlineExamSystem.Data.Configs;

public class QuestionOptionsConfigurations : IEntityTypeConfiguration<QuestionOptions>
{
    public void Configure(EntityTypeBuilder<QuestionOptions> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Question)
               .WithMany(x => x.QuestionOptions)
               .HasForeignKey(x => x.QuestionId)
               .IsRequired();
        builder.ToTable("QuestionOptions");
    }
}
