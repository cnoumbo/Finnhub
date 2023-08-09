using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinnhubApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buyorders",
                columns: table => new
                {
                    BuyOrderID = table.Column<Guid>(type: "uuid", nullable: false),
                    StockSymbol = table.Column<string>(type: "text", nullable: false),
                    StockName = table.Column<string>(type: "text", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyorders", x => x.BuyOrderID);
                });

            migrationBuilder.CreateTable(
                name: "sellorders",
                columns: table => new
                {
                    SellOrderID = table.Column<Guid>(type: "uuid", nullable: false),
                    StockSymbol = table.Column<string>(type: "text", nullable: false),
                    StockName = table.Column<string>(type: "text", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellorders", x => x.SellOrderID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "buyorders");

            migrationBuilder.DropTable(
                name: "sellorders");
        }
    }
}
