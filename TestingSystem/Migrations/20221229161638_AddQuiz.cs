using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingSystem.Migrations
{
    public partial class AddQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccumulateTime",
                table: "TriviaQuizs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LivesCount",
                table: "TriviaQuizs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionTime",
                table: "TriviaQuizs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccumulateTime",
                table: "TriviaQuizs");

            migrationBuilder.DropColumn(
                name: "LivesCount",
                table: "TriviaQuizs");

            migrationBuilder.DropColumn(
                name: "QuestionTime",
                table: "TriviaQuizs");
        }
    }
}
