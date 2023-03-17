using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingSystem.Migrations
{
    public partial class sfdsdfgfdgааывfdsg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TriviaAnswers",
                newName: "Answer");

            migrationBuilder.RenameColumn(
                name: "TriviaQuizId",
                table: "TriviaAnswers",
                newName: "QuestionId");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "TriviaQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "TriviaQuestions");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "TriviaAnswers",
                newName: "TriviaQuizId");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "TriviaAnswers",
                newName: "UserId");
        }
    }
}
