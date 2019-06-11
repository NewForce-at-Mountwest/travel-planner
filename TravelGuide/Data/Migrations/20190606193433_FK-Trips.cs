using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelGuide.Data.Migrations
{
    public partial class FKTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Client",
                table: "Trip",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Trip",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_ClientId",
                table: "Trip",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Client_ClientId",
                table: "Trip",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Client_ClientId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_ClientId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Trip");
        }
    }
}
