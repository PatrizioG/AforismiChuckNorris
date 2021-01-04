using Microsoft.EntityFrameworkCore.Migrations;

namespace AforismiChuckNorris.Data.Migrations
{
    public partial class AddCulture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Culture",
                table: "Aphorisms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Culture",
                table: "Aphorisms");
        }
    }
}
