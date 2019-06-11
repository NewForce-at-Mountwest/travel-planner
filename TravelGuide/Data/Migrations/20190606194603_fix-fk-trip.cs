using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelGuide.Data.Migrations
{
    public partial class fixfktrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Client_ClientId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "Trip");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Trip",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Client_ClientId",
                table: "Trip",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Client_ClientId",
                table: "Trip");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Trip",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Client",
                table: "Trip",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Client_ClientId",
                table: "Trip",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
