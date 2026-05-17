using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Infrastructure.EF.EntityConfigurations
{
    public class RestaurantMonthlyReportEntityConfiguration : IEntityTypeConfiguration<RestaurantMonthlyReport>
    {
        public void Configure(EntityTypeBuilder<RestaurantMonthlyReport> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Year)
                .IsRequired();

            builder.Property(x => x.Month)
                .IsRequired();

            builder.Property(x => x.Revenue)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Rating)
                .IsRequired()
                .HasColumnType("decimal(3,2)");

            builder.HasIndex(x => new { x.RestaurantId, x.Year, x.Month })
                .IsUnique();

            builder.HasOne(x => x.Restaurant)
                .WithMany(x => x.MonthlyReports)
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
