using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class AddrelationInvoiceUserAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAddressId",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserAddressId",
                table: "Invoices",
                column: "UserAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_UserAddress_UserAddressId",
                table: "Invoices",
                column: "UserAddressId",
                principalTable: "UserAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_UserAddress_UserAddressId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_UserAddressId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UserAddressId",
                table: "Invoices");
        }
    }
}
