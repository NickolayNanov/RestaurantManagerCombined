using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Infrastructure.EF.EntityConfigurations
{
    public class MenuItemCategoryEntityConfiguration : IEntityTypeConfiguration<MenuItemCategory>
    {
        public void Configure(EntityTypeBuilder<MenuItemCategory> builder)
        {
            builder.HasKey(x => new { x.MenuItemId, x.CategoryId });

            builder.HasOne(mic => mic.MenuItem)
                .WithMany(mi => mi.MenuItemCategories)
                .HasForeignKey(mic => mic.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mic => mic.Category)
                .WithMany(c => c.MenuItemCategories)
                .HasForeignKey(mic => mic.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
