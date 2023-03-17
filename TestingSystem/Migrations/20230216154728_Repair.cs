using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingSystem.Migrations
{
    public partial class Repair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TriviaAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TriviaQuizs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LivesCount",
                table: "TriviaQuizs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccumulateTime",
                table: "TriviaQuizs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ActiveTrivias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTrivias_TriviaQuizId",
                table: "ActiveTrivias",
                column: "TriviaQuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveTrivias_TriviaQuizs_TriviaQuizId",
                table: "ActiveTrivias",
                column: "TriviaQuizId",
                principalTable: "TriviaQuizs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveTrivias_TriviaQuizs_TriviaQuizId",
                table: "ActiveTrivias");

            migrationBuilder.DropIndex(
                name: "IX_ActiveTrivias_TriviaQuizId",
                table: "ActiveTrivias");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ActiveTrivias");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TriviaQuizs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LivesCount",
                table: "TriviaQuizs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccumulateTime",
                table: "TriviaQuizs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TriviaAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriviaAnswers", x => x.Id);
                });
        }
    }
}
