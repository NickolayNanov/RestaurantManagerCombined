using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_status_imgUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Employees");
        }
    }
}
