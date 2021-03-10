using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class RemoveSomeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookComment_Users_UserId",
                table: "BookComment");

            migrationBuilder.DropIndex(
                name: "IX_BookComment_UserId",
                table: "BookComment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookComment_UserId",
                table: "BookComment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookComment_Users_UserId",
                table: "BookComment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
