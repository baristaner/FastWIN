using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastwin.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCode_Codes_CodesId",
                table: "UserCode");

            migrationBuilder.DropIndex(
                name: "IX_UserCode_CodesId",
                table: "UserCode");

            migrationBuilder.DropColumn(
                name: "CodesId",
                table: "UserCode");

            migrationBuilder.AddColumn<int>(
                name: "CodeId",
                table: "UserCode",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserCode_CodeId",
                table: "UserCode",
                column: "CodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCode_Codes_CodeId",
                table: "UserCode",
                column: "CodeId",
                principalTable: "Codes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCode_Codes_CodeId",
                table: "UserCode");

            migrationBuilder.DropIndex(
                name: "IX_UserCode_CodeId",
                table: "UserCode");

            migrationBuilder.DropColumn(
                name: "CodeId",
                table: "UserCode");

            migrationBuilder.AddColumn<int>(
                name: "CodesId",
                table: "UserCode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCode_CodesId",
                table: "UserCode",
                column: "CodesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCode_Codes_CodesId",
                table: "UserCode",
                column: "CodesId",
                principalTable: "Codes",
                principalColumn: "Id");
        }
    }
}
