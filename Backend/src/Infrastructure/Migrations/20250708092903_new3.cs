using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalyzeCriterions_AnalysisDocuments_AnalysisDocumentId",
                table: "AnalyzeCriterions");

            migrationBuilder.DropIndex(
                name: "IX_AnalyzeCriterions_AnalysisDocumentId",
                table: "AnalyzeCriterions");

            migrationBuilder.DropColumn(
                name: "AnalysisDocumentId",
                table: "AnalyzeCriterions");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisCriteriaAnalysisDocument");

            migrationBuilder.AddColumn<Guid>(
                name: "AnalysisDocumentId",
                table: "AnalyzeCriterions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzeCriterions_AnalysisDocumentId",
                table: "AnalyzeCriterions",
                column: "AnalysisDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalyzeCriterions_AnalysisDocuments_AnalysisDocumentId",
                table: "AnalyzeCriterions",
                column: "AnalysisDocumentId",
                principalTable: "AnalysisDocuments",
                principalColumn: "Id");
        }
    }
}
