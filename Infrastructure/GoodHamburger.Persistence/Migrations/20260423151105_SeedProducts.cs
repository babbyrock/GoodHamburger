using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodHamburger.Persistence.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price", "Type", "CreatedAt" },
                values: new object[,]
                {
                    { 1L, "X Burger",     5.00m, "Sandwich", DateTime.UtcNow },
                    { 2L, "X Egg",        4.50m, "Sandwich", DateTime.UtcNow },
                    { 3L, "X Bacon",      7.00m, "Sandwich", DateTime.UtcNow },
                    { 4L, "Batata Frita", 2.00m, "Fries",    DateTime.UtcNow },
                    { 5L, "Refrigerante", 2.50m, "Drink",    DateTime.UtcNow }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValues: new object[] { 1L, 2L, 3L, 4L, 5L });
        }
    }
}