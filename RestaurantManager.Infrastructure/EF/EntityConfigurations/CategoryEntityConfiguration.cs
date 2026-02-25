using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Infrastructure.EF.EntityConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(75);

            builder.Property(c => c.IsActive)
                .IsRequired();

            builder.HasMany(c => c.MenuItemCategories)
                .WithOne(mic => mic.Category)
                .HasForeignKey(mic => mic.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
