using Entity.Domain.Models.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Infrastructure.DataInit
{
    public class ModuleSeeder : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd(); // La base de datos generará el ID automáticamente
            builder.Property(u => u.Name)
                .HasColumnType("varchar(100)");

            builder.Property(u => u.Description)
                .HasColumnType("varchar(100)");

            builder.HasData(
                new Module
                {
                    Id = 1,
                    Name = "Module User",
                    Description = "Module for User",
                    Active = true,
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Module
                {
                    Id = 2,
                    Name = "Module Rol",
                    Description = "Module for Rol",
                    Active = true,
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)

                }
            );
        }
    }
}
