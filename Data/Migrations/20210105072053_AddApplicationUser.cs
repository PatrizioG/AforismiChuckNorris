using Microsoft.EntityFrameworkCore.Migrations;

namespace AforismiChuckNorris.Data.Migrations
{
    public partial class AddApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPendingRequest",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Aphorisms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Aphorisms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aphorisms_UserId",
                table: "Aphorisms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aphorisms_AspNetUsers_UserId",
                table: "Aphorisms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aphorisms_AspNetUsers_UserId",
                table: "Aphorisms");

            migrationBuilder.DropIndex(
                name: "IX_Aphorisms_UserId",
                table: "Aphorisms");

            migrationBuilder.DropColumn(
                name: "MaxPendingRequest",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Aphorisms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Aphorisms");
        }
    }
}
