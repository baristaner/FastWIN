using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastwin.Migrations
{
    /// <inheritdoc />
    public partial class UserEntitiesUpdateTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_UserCodes_CodeId",
                table: "UserCodes",
                column: "CodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCodes_Codes_CodeId",
                table: "UserCodes",
                column: "CodeId",
                principalTable: "Codes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCodes_Codes_CodeId",
                table: "UserCodes");

            migrationBuilder.DropIndex(
                name: "IX_UserCodes_CodeId",
                table: "UserCodes");

            migrationBuilder.AddColumn<int>(
                name: "CodesId",
                table: "UserCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserCodes_CodesId",
                table: "UserCodes",
                column: "CodesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCodes_Codes_CodesId",
                table: "UserCodes",
                column: "CodesId",
                principalTable: "Codes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
