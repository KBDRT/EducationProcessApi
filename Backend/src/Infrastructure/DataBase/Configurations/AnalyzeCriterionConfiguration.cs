using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcessAPI.Infrastructure.DataBase.Configurations
{
    public class AnalyzeCriterionConfiguration : IEntityTypeConfiguration<AnalysisCriteria>
    {
        public void Configure(EntityTypeBuilder<AnalysisCriteria> builder)
        {
            builder.HasMany(e => e.Options)
                    .WithOne(e => e.Criterion)
                    .HasForeignKey("CriterionId")
                    .IsRequired();
        }
    }
}
