using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAnswersFromQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "QuestionOptions");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tests",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tests",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "QuestionOptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
