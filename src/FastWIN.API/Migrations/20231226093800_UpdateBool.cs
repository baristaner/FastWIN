using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastwin.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modifiedAt",
                table: "Codes",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Codes",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CODE",
                table: "Codes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Codes",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Codes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Codes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Codes");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Codes",
                newName: "modifiedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Codes",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Codes",
                newName: "CODE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Codes",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "CODE",
                table: "Codes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
