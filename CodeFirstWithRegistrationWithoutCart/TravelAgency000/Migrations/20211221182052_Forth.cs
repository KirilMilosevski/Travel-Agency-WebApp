using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgency000.Migrations
{
    public partial class Forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Destination");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Destination",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChooseDestination",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemPrice = table.Column<int>(type: "int", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChooseDestination", x => x.ItemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChooseDestination");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Destination");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Destination",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
