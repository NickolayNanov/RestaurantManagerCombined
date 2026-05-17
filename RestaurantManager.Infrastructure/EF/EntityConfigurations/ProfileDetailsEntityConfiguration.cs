using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Infrastructure.EF.EntityConfigurations
{
    public class ProfileDetailsEntityConfiguration : IEntityTypeConfiguration<ProfileDetails>
    {
        public void Configure(EntityTypeBuilder<ProfileDetails> builder)
        {
            builder.Property(e => e.ProfilePictureUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.HasIndex(e => e.UserId)
                .IsUnique();

            builder.HasOne(e => e.User)
                .WithOne(u => u.ProfileDetails)
                .HasForeignKey<ProfileDetails>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
