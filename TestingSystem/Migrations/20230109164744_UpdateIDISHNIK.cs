using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingSystem.Migrations
{
    public partial class UpdateIDISHNIK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TriviaQuestions_TriviaQuizs_TriviaQuizId",
                table: "TriviaQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "TriviaQuizId",
                table: "TriviaQuestions",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TriviaQuestions_TriviaQuizs_TriviaQuizId",
                table: "TriviaQuestions",
                column: "TriviaQuizId",
                principalTable: "TriviaQuizs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TriviaQuestions_TriviaQuizs_TriviaQuizId",
                table: "TriviaQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "TriviaQuizId",
                table: "TriviaQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TriviaQuestions_TriviaQuizs_TriviaQuizId",
                table: "TriviaQuestions",
                column: "TriviaQuizId",
                principalTable: "TriviaQuizs",
                principalColumn: "Id");
        }
    }
}
