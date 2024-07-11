using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedReferenceKeyToUserForTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Tests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Tests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_UserId1",
                table: "Tests",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AspNetUsers_UserId1",
                table: "Tests",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AspNetUsers_UserId1",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_UserId1",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Tests");
        }
    }
}
