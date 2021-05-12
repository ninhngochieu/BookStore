using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class RefactorInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "CityAddressId",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistrictAddressId",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CityAddressId",
                table: "Invoices",
                column: "CityAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DistrictAddressId",
                table: "Invoices",
                column: "DistrictAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_WardId",
                table: "Invoices",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_CityAddresses_CityAddressId",
                table: "Invoices",
                column: "CityAddressId",
                principalTable: "CityAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_DistrictAddresses_DistrictAddressId",
                table: "Invoices",
                column: "DistrictAddressId",
                principalTable: "DistrictAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Ward_WardId",
                table: "Invoices",
                column: "WardId",
                principalTable: "Ward",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_CityAddresses_CityAddressId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_DistrictAddresses_DistrictAddressId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Ward_WardId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CityAddressId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_DistrictAddressId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_WardId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CityAddressId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DistrictAddressId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Invoices",
                type: "TEXT",
                nullable: true);
        }
    }
}
