using EducationProcessAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

            builder.OwnsOne(c => c.Initials,
            sa =>
            {
                sa.Property(p => p.Surname).HasColumnName("Surname");
                sa.Property(p => p.Name).HasColumnName("Name");
                sa.Property(p => p.Patronymic).HasColumnName("Patronymic");
            });
        }
    }
}
