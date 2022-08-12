using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicProject.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusicPath128",
                table: "Music");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MusicPath128",
                table: "Music",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
