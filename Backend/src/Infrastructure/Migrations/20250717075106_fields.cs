using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisCriteriaAnalysisDocument");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "AnalysisCriteriaAnalysisDocument",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedCriteriasId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisCriteriaAnalysisDocument", x => new { x.DocumentId, x.SelectedCriteriasId });
                    table.ForeignKey(
                        name: "FK_AnalysisCriteriaAnalysisDocument_AnalysisDocuments_Document~",
                        column: x => x.DocumentId,
                        principalTable: "AnalysisDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalysisCriteriaAnalysisDocument_AnalyzeCriterions_Selected~",
                        column: x => x.SelectedCriteriasId,
                        principalTable: "AnalyzeCriterions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisCriteriaAnalysisDocument_SelectedCriteriasId",
                table: "AnalysisCriteriaAnalysisDocument",
                column: "SelectedCriteriasId");
        }
    }
}
