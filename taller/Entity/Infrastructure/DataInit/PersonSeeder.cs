using Entity.Domain.Models.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Infrastructure.DataInit
{
    public class PersonSeeder : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd(); // La base de datos generará el ID automáticamente

            builder.Property(u => u.FirstName)
                .HasColumnType("varchar(100)");

            builder.Property(u => u.LastName)
                .HasColumnType("varchar(100)");

            builder.Property(u => u.PhoneNumber)
                .HasColumnType("varchar(20)");

            builder.Property(u => u.Address)
                .HasColumnType("varchar(100)");

            builder.HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "1234567890",
                    Address = "AV SiempreViva",
                    Active = true,
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Person
                {
                    Id = 2,
                    FirstName = "User",
                    LastName = "User",
                    PhoneNumber = "1234567890",
                    Address = "AV SiempreViva",
                    Active = true,
                    IsDeleted = false,
                    CreatedAt = new DateTime(2025, 1, 1)
                }

            );
        }
    }
}
