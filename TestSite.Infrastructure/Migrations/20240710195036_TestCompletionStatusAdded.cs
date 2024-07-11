using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestCompletionStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestCompletionStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimePassed = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Retries = table.Column<int>(type: "integer", nullable: false),
                    IsOver = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCompletionStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCompletionStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestCompletionStatuses_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCompletionStatuses_TestId",
                table: "TestCompletionStatuses",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCompletionStatuses_UserId",
                table: "TestCompletionStatuses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestCompletionStatuses");
        }
    }
}
