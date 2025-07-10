using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataBase.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Roles).WithMany(x => x.Users);


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
