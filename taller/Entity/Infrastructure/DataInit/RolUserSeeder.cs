using Entity.Domain.Models.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Infrastructure.DataInit
{
    public class RolUserSeeder : IEntityTypeConfiguration<RolUser>
    {
        public void Configure(EntityTypeBuilder<RolUser> builder)
        {
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd(); // La base de datos generará el ID automáticamente
            builder.HasData(
                new RolUser
                {
                    Id = 1,
                    UserId = 1,
                    RolId = 1,
                    Active = true,
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)
                }
            );
        }
    }
}
