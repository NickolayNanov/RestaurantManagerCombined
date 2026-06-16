using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeduserdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO ProfileDetails (
                    Id,
                    UserId,
                    CreatedAt
                )
                SELECT
                    NEWID(),
                    u.Id,
                    SYSUTCDATETIME()
                FROM AspNetUsers u
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM ProfileDetails pd
                    WHERE pd.UserId = u.Id
                );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE pd
                FROM ProfileDetails pd
                WHERE pd.ProfilePictureUrl IS NULL
                    AND pd.FirstName IS NULL
                    AND pd.Surname IS NULL
                    AND pd.LastName IS NULL
                    AND pd.CompanyName IS NULL
                    AND pd.PhoneNumber IS NULL
                    AND pd.CreatedBy IS NULL
                    AND pd.UpdatedAt IS NULL
                    AND pd.UpdatedBy IS NULL;
                """);
        }
    }
}
