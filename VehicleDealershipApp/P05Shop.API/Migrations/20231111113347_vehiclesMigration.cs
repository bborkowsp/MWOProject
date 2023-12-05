using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P05_3Shop.API.Migrations
{
    /// <inheritdoc />
    public partial class vehiclesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fuel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Fuel", "Model" },
                values: new object[,]
                {
                    { 1, "Diesel", "Colorado" },
                    { 2, "Electric", "Focus" },
                    { 3, "Electric", "Spyder" },
                    { 4, "Electric", "Malibu" },
                    { 5, "Hybrid", "Roadster" },
                    { 6, "Electric", "Expedition" },
                    { 7, "Diesel", "Camry" },
                    { 8, "Hybrid", "Altima" },
                    { 9, "Gasoline", "Altima" },
                    { 10, "Diesel", "Element" },
                    { 11, "Diesel", "Durango" },
                    { 12, "Hybrid", "Element" },
                    { 13, "Electric", "Expedition" },
                    { 14, "Diesel", "Explorer" },
                    { 15, "Gasoline", "Sentra" },
                    { 16, "Gasoline", "Alpine" },
                    { 17, "Diesel", "1" },
                    { 18, "Diesel", "Spyder" },
                    { 19, "Electric", "Altima" },
                    { 20, "Gasoline", "LeBaron" },
                    { 21, "Gasoline", "Cruze" },
                    { 22, "Diesel", "Mustang" },
                    { 23, "Diesel", "ATS" },
                    { 24, "Gasoline", "Sentra" },
                    { 25, "Electric", "Focus" },
                    { 26, "Electric", "Challenger" },
                    { 27, "Hybrid", "Malibu" },
                    { 28, "Diesel", "A4" },
                    { 29, "Diesel", "PT Cruiser" },
                    { 30, "Gasoline", "Colorado" },
                    { 31, "Diesel", "Charger" },
                    { 32, "Hybrid", "Sentra" },
                    { 33, "Electric", "LeBaron" },
                    { 34, "Electric", "PT Cruiser" },
                    { 35, "Gasoline", "Colorado" },
                    { 36, "Diesel", "Volt" },
                    { 37, "Diesel", "Impala" },
                    { 38, "Gasoline", "1" },
                    { 39, "Gasoline", "Explorer" },
                    { 40, "Diesel", "Grand Cherokee" },
                    { 41, "Gasoline", "Land Cruiser" },
                    { 42, "Electric", "CX-9" },
                    { 43, "Diesel", "F-150" },
                    { 44, "Diesel", "Charger" },
                    { 45, "Gasoline", "ATS" },
                    { 46, "Gasoline", "Civic" },
                    { 47, "Hybrid", "Impala" },
                    { 48, "Hybrid", "Corvette" },
                    { 49, "Diesel", "Taurus" },
                    { 50, "Hybrid", "Land Cruiser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
