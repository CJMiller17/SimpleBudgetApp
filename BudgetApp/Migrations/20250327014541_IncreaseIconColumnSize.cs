using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseIconColumnSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Categories",
                type: "nvarchar(5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");
        }
    }
}
