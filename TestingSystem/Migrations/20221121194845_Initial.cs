using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingSystem.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TriviaAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriviaQuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriviaAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TriviaQuizs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriviaQuizs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TriviaQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriviaQuizId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriviaQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriviaQuestions_TriviaQuizs_TriviaQuizId",
                        column: x => x.TriviaQuizId,
                        principalTable: "TriviaQuizs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TriviaOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriviaQuestionId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriviaOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriviaOptions_TriviaQuestions_TriviaQuestionId",
                        column: x => x.TriviaQuestionId,
                        principalTable: "TriviaQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TriviaOptions_TriviaQuestionId",
                table: "TriviaOptions",
                column: "TriviaQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TriviaQuestions_TriviaQuizId",
                table: "TriviaQuestions",
                column: "TriviaQuizId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TriviaAnswers");

            migrationBuilder.DropTable(
                name: "TriviaOptions");

            migrationBuilder.DropTable(
                name: "TriviaQuestions");

            migrationBuilder.DropTable(
                name: "TriviaQuizs");
        }
    }
}
