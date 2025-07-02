using EducationProcessAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Infrastructure.DataBase.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasMany(t => t.Union)  
                           .WithOne(u => u.Teacher)
                           .HasForeignKey("TeacherId")
                           .IsRequired();

        }
    }
}
