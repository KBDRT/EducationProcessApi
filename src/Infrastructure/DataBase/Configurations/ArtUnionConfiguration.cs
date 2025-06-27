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
    public class ArtUnionConfiguration : IEntityTypeConfiguration<ArtUnion>
    {
        public void Configure(EntityTypeBuilder<ArtUnion> builder)
        {
            builder.HasOne(e => e.Direction)
                                .WithMany()
                                .IsRequired();



        }
    }



}
