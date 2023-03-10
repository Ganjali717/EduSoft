using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduSoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateSubChapterIntro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntroImages_SubChapterIntro_SubChapterIntroId",
                table: "IntroImages");

            migrationBuilder.DropForeignKey(
                name: "FK_SubChapterIntro_Subchapters_SubChapterId",
                table: "SubChapterIntro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubChapterIntro",
                table: "SubChapterIntro");

            migrationBuilder.RenameTable(
                name: "SubChapterIntro",
                newName: "SubchIntros");

            migrationBuilder.RenameIndex(
                name: "IX_SubChapterIntro_SubChapterId",
                table: "SubchIntros",
                newName: "IX_SubchIntros_SubChapterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubchIntros",
                table: "SubchIntros",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntroImages_SubchIntros_SubChapterIntroId",
                table: "IntroImages",
                column: "SubChapterIntroId",
                principalTable: "SubchIntros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubchIntros_Subchapters_SubChapterId",
                table: "SubchIntros",
                column: "SubChapterId",
                principalTable: "Subchapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntroImages_SubchIntros_SubChapterIntroId",
                table: "IntroImages");

            migrationBuilder.DropForeignKey(
                name: "FK_SubchIntros_Subchapters_SubChapterId",
                table: "SubchIntros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubchIntros",
                table: "SubchIntros");

            migrationBuilder.RenameTable(
                name: "SubchIntros",
                newName: "SubChapterIntro");

            migrationBuilder.RenameIndex(
                name: "IX_SubchIntros_SubChapterId",
                table: "SubChapterIntro",
                newName: "IX_SubChapterIntro_SubChapterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubChapterIntro",
                table: "SubChapterIntro",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntroImages_SubChapterIntro_SubChapterIntroId",
                table: "IntroImages",
                column: "SubChapterIntroId",
                principalTable: "SubChapterIntro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubChapterIntro_Subchapters_SubChapterId",
                table: "SubChapterIntro",
                column: "SubChapterId",
                principalTable: "Subchapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
