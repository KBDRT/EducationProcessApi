using EducationProcessAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace EducationProcessAPI.Infrastructure.DataBase.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasMany(e => e.Lessons)
                    .WithOne(e => e.Group)
                    .HasForeignKey("GroupId")
                    .IsRequired();
        }
    }
}
