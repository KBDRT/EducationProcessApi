using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeUserTab3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Initials_Surname",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Initials_Patronymic",
                table: "Users",
                newName: "Patronymic");

            migrationBuilder.RenameColumn(
                name: "Initials_Name",
                table: "Users",
                newName: "Name");

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

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "Initials_Surname");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "Users",
                newName: "Initials_Patronymic");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Initials_Name");

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
    }
}
