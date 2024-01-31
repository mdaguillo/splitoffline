using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRecurringExpenseAddIsPaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Share",
                table: "ExpenseParticipants");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RecurringExpense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IntervalDays = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringExpense", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Description",
                table: "Expenses",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_IsPaid",
                table: "Expenses",
                column: "IsPaid");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringExpense_Description",
                table: "RecurringExpense",
                column: "Description",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringExpense");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_Description",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_IsPaid",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Expenses");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<decimal>(
                name: "Share",
                table: "ExpenseParticipants",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
