using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Infrastructure.EF.EntityConfigurations
{
    public class RestaurantEntityConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);
        
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .HasMaxLength(500);

            builder.Property(r => r.Location)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Cuisine)
                .IsRequired()
                .HasMaxLength(75);

            builder.HasOne(r => r.Owner)
                .WithMany(o => o.Restaurants)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

             builder.HasMany(r => r.Menus)
                .WithOne(m => m.Restaurant)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
