using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastwin.Migrations
{
    /// <inheritdoc />
    public partial class JWTAuthList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "UserCodes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCodes_UsersId",
                table: "UserCodes",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCodes_AspNetUsers_UsersId",
                table: "UserCodes",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCodes_AspNetUsers_UsersId",
                table: "UserCodes");

            migrationBuilder.DropIndex(
                name: "IX_UserCodes_UsersId",
                table: "UserCodes");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "UserCodes");
        }
    }
}
