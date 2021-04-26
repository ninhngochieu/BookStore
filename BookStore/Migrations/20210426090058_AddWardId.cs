using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class AddWardId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "UserAddress",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_WardId",
                table: "UserAddress",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Ward_WardId",
                table: "UserAddress",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Ward_WardId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_WardId",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "UserAddress");
        }
    }
}
