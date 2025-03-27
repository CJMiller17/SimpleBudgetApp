using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedColumnTypeOnTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
