using Entity.Domain.Models.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Infrastructure.DataInit
{
    internal class FormModuleSeeder : IEntityTypeConfiguration<FormModule>
    {
        public void Configure(EntityTypeBuilder<FormModule> builder)
        {
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd(); // La base de datos generará el ID automáticamente
            builder.HasData(
                new FormModule
                { 
                    Id = 1,
                    FormId = 1,
                    ModuleId = 1,
                    Active = true,
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new FormModule
                {
                    Id = 2,
                    FormId = 2,
                    ModuleId = 2,
                    Active = true,
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)

                }
            );
        }
    }
}
