using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicProject.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistMusic_Artists_Artistsid",
                table: "ArtistMusic");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistMusic_Music_MusicsId",
                table: "ArtistMusic");

            migrationBuilder.RenameColumn(
                name: "MusicsId",
                table: "ArtistMusic",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "Artistsid",
                table: "ArtistMusic",
                newName: "MusicId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistMusic_MusicsId",
                table: "ArtistMusic",
                newName: "IX_ArtistMusic_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistMusic_Artists_ArtistId",
                table: "ArtistMusic",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistMusic_Music_MusicId",
                table: "ArtistMusic",
                column: "MusicId",
                principalTable: "Music",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistMusic_Artists_ArtistId",
                table: "ArtistMusic");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistMusic_Music_MusicId",
                table: "ArtistMusic");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "ArtistMusic",
                newName: "MusicsId");

            migrationBuilder.RenameColumn(
                name: "MusicId",
                table: "ArtistMusic",
                newName: "Artistsid");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistMusic_ArtistId",
                table: "ArtistMusic",
                newName: "IX_ArtistMusic_MusicsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistMusic_Artists_Artistsid",
                table: "ArtistMusic",
                column: "Artistsid",
                principalTable: "Artists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistMusic_Music_MusicsId",
                table: "ArtistMusic",
                column: "MusicsId",
                principalTable: "Music",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
