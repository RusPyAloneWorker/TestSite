using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyAnsweredQuestionsWithOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_AnsweredQuestions_AnsweredQuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionOptions_AnsweredQuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.DropColumn(
                name: "AnsweredQuestionModelId",
                table: "QuestionOptions");

            migrationBuilder.CreateTable(
                name: "AnsweredQuestionModelQuestionOptionModel",
                columns: table => new
                {
                    AnsweredQuestionModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionOptionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnsweredQuestionModelQuestionOptionModel", x => new { x.AnsweredQuestionModelId, x.QuestionOptionsId });
                    table.ForeignKey(
                        name: "FK_AnsweredQuestionModelQuestionOptionModel_AnsweredQuestions_~",
                        column: x => x.AnsweredQuestionModelId,
                        principalTable: "AnsweredQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnsweredQuestionModelQuestionOptionModel_QuestionOptions_Qu~",
                        column: x => x.QuestionOptionsId,
                        principalTable: "QuestionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredQuestionModelQuestionOptionModel_QuestionOptionsId",
                table: "AnsweredQuestionModelQuestionOptionModel",
                column: "QuestionOptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnsweredQuestionModelQuestionOptionModel");

            migrationBuilder.AddColumn<Guid>(
                name: "AnsweredQuestionModelId",
                table: "QuestionOptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_AnsweredQuestionModelId",
                table: "QuestionOptions",
                column: "AnsweredQuestionModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_AnsweredQuestions_AnsweredQuestionModelId",
                table: "QuestionOptions",
                column: "AnsweredQuestionModelId",
                principalTable: "AnsweredQuestions",
                principalColumn: "Id");
        }
    }
}
