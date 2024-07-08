using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestModelId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TestModelId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionOptions_QuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.DropColumn(
                name: "Answers",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestModelId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "TestId",
                table: "Questions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "QuestionOptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "QuestionOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TestId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "QuestionOptions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "QuestionOptions");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Answers",
                table: "Questions",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TestModelId",
                table: "Questions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionModelId",
                table: "QuestionOptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestModelId",
                table: "Questions",
                column: "TestModelId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionModelId",
                table: "QuestionOptions",
                column: "QuestionModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_Questions_QuestionModelId",
                table: "QuestionOptions",
                column: "QuestionModelId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestModelId",
                table: "Questions",
                column: "TestModelId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
