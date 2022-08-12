using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicProject.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MusicPath",
                table: "Music",
                newName: "MusicPath320");

            migrationBuilder.AddColumn<string>(
                name: "MusicPath128",
                table: "Music",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MusicPath128",
                table: "Music");

            migrationBuilder.RenameColumn(
                name: "MusicPath320",
                table: "Music",
                newName: "MusicPath");
        }
    }
}
