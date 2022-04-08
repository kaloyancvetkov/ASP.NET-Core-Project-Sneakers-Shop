using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakersShop.Data.Migrations
{
    public partial class SneakersTableIsPublicColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPublic",
                table: "Sneakers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPublic",
                table: "Sneakers");
        }
    }
}
