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
    internal class ArtDirectionConfiguration : IEntityTypeConfiguration<ArtDirection>
    {
        public void Configure(EntityTypeBuilder<ArtDirection> builder)
        {
    

        }
    }
}
