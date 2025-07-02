using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnalysisDocumentId",
                table: "AnalyzeCriterions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnalysisDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnalysisTarget = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArtUnionId = table.Column<Guid>(type: "uuid", nullable: true),
                    LessonId = table.Column<Guid>(type: "uuid", nullable: true),
                    CheckDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ResultDescription = table.Column<string>(type: "text", nullable: false),
                    AuditorName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisDocuments_ArtUnions_ArtUnionId",
                        column: x => x.ArtUnionId,
                        principalTable: "ArtUnions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnalysisDocuments_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnalysisDocuments_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzeCriterions_AnalysisDocumentId",
                table: "AnalyzeCriterions",
                column: "AnalysisDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisDocuments_ArtUnionId",
                table: "AnalysisDocuments",
                column: "ArtUnionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisDocuments_LessonId",
                table: "AnalysisDocuments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisDocuments_TeacherId",
                table: "AnalysisDocuments",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzeCriterions_AnalysisDocuments_AnalysisDocumentId",
                table: "AnalyzeCriterions",
                column: "AnalysisDocumentId",
                principalTable: "AnalysisDocuments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzeCriterions_AnalysisDocuments_AnalysisDocumentId",
                table: "AnalyzeCriterions");

            migrationBuilder.DropTable(
                name: "AnalysisDocuments");

            migrationBuilder.DropIndex(
                name: "IX_AnalyzeCriterions_AnalysisDocumentId",
                table: "AnalyzeCriterions");

            migrationBuilder.DropColumn(
                name: "AnalysisDocumentId",
                table: "AnalyzeCriterions");
        }
    }
}
