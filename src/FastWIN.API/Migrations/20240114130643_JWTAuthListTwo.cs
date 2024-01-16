using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastwin.Migrations
{
    /// <inheritdoc />
    public partial class JWTAuthListTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodesId",
                table: "UserCodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCodes_CodesId",
                table: "UserCodes",
                column: "CodesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCodes_Codes_CodesId",
                table: "UserCodes",
                column: "CodesId",
                principalTable: "Codes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCodes_Codes_CodesId",
                table: "UserCodes");

            migrationBuilder.DropIndex(
                name: "IX_UserCodes_CodesId",
                table: "UserCodes");

            migrationBuilder.DropColumn(
                name: "CodesId",
                table: "UserCodes");
        }
    }
}
