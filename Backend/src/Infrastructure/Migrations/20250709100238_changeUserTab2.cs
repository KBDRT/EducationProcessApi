using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeUserTab2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Teachers",
                newName: "Initials_Surname");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "Teachers",
                newName: "Initials_Patronymic");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teachers",
                newName: "Initials_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Initials_Surname",
                table: "Teachers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Initials_Patronymic",
                table: "Teachers",
                newName: "Patronymic");

            migrationBuilder.RenameColumn(
                name: "Initials_Name",
                table: "Teachers",
                newName: "Name");
        }
    }
}
