using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Infrastructure.EF.EntityConfigurations
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.EmploymentType)
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired();

            builder.Property(e => e.Salary)
                .IsRequired();

            builder.Property(e => e.ImgUrl)
                .IsRequired();

            builder.HasOne(e => e.Restaurant)
                .WithMany(r => r.Employees)
                .HasForeignKey(fk => fk.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
