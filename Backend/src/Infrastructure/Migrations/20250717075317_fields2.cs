using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fields2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalysisDocuments_AnalyzeCriterions_AnalysisCriteriaId",
                table: "AnalysisDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_CriterionOptions_AnalysisDocuments_AnalysisDocumentId",
                table: "CriterionOptions");

            migrationBuilder.DropIndex(
                name: "IX_CriterionOptions_AnalysisDocumentId",
                table: "CriterionOptions");

            migrationBuilder.DropIndex(
                name: "IX_AnalysisDocuments_AnalysisCriteriaId",
                table: "AnalysisDocuments");

            migrationBuilder.DropColumn(
                name: "AnalysisDocumentId",
                table: "CriterionOptions");

            migrationBuilder.DropColumn(
                name: "AnalysisCriteriaId",
                table: "AnalysisDocuments");

            migrationBuilder.CreateTable(
                name: "AnalysisDocumentCriterionOption",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedOptionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisDocumentCriterionOption", x => new { x.DocumentId, x.SelectedOptionsId });
                    table.ForeignKey(
                        name: "FK_AnalysisDocumentCriterionOption_AnalysisDocuments_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "AnalysisDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalysisDocumentCriterionOption_CriterionOptions_SelectedOp~",
                        column: x => x.SelectedOptionsId,
                        principalTable: "CriterionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisDocumentCriterionOption_SelectedOptionsId",
                table: "AnalysisDocumentCriterionOption",
                column: "SelectedOptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisDocumentCriterionOption");

            migrationBuilder.AddColumn<Guid>(
                name: "AnalysisDocumentId",
                table: "CriterionOptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AnalysisCriteriaId",
                table: "AnalysisDocuments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CriterionOptions_AnalysisDocumentId",
                table: "CriterionOptions",
                column: "AnalysisDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisDocuments_AnalysisCriteriaId",
                table: "AnalysisDocuments",
                column: "AnalysisCriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalysisDocuments_AnalyzeCriterions_AnalysisCriteriaId",
                table: "AnalysisDocuments",
                column: "AnalysisCriteriaId",
                principalTable: "AnalyzeCriterions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CriterionOptions_AnalysisDocuments_AnalysisDocumentId",
                table: "CriterionOptions",
                column: "AnalysisDocumentId",
                principalTable: "AnalysisDocuments",
                principalColumn: "Id");
        }
    }
}
