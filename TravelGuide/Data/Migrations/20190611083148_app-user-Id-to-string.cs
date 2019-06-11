using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelGuide.Data.Migrations
{
    public partial class appuserIdtostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Client",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Client",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
