using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class CityAndDistrictAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityName = table.Column<string>(type: "TEXT", nullable: true),
                    CityCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistrictAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DistrictName = table.Column<string>(type: "TEXT", nullable: true),
                    CityAddressId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistrictAddresses_CityAddresses_CityAddressId",
                        column: x => x.CityAddressId,
                        principalTable: "CityAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistrictAddresses_CityAddressId",
                table: "DistrictAddresses",
                column: "CityAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistrictAddresses");

            migrationBuilder.DropTable(
                name: "CityAddresses");
        }
    }
}
