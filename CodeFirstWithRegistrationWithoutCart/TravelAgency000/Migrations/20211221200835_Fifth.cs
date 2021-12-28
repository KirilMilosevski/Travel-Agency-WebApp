using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgency000.Migrations
{
    public partial class Fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "ChooseDestination");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "ChooseDestination",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "ChooseDestination");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "ChooseDestination",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
