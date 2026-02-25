using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Infrastructure.EF.EntityConfigurations
{
    public class MenuItemyEntityConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(mi => mi.Id);
            
            builder.Property(mi => mi.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(mi => mi.Price)
                .IsRequired();

            builder.Property(mi => mi.IsActive)
                .IsRequired();

            builder.HasOne(builder => builder.Menu)
                .WithMany(m => m.MenuItems)
                .HasForeignKey(mi => mi.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(mi => mi.MenuItemCategories)
                .WithOne(mic => mic.MenuItem)
                .HasForeignKey(mic => mic.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
