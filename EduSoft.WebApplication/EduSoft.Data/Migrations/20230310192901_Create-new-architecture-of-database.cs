using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduSoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class Createnewarchitectureofdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubchIntros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubchIntros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubchIntros_Subchapters_SubChapterId",
                        column: x => x.SubChapterId,
                        principalTable: "Subchapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntroImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubChapterIntroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntroImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntroImages_SubchIntros_SubChapterIntroId",
                        column: x => x.SubChapterIntroId,
                        principalTable: "SubchIntros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntroImages_SubChapterIntroId",
                table: "IntroImages",
                column: "SubChapterIntroId");

            migrationBuilder.CreateIndex(
                name: "IX_SubchIntros_SubChapterId",
                table: "SubchIntros",
                column: "SubChapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntroImages");

            migrationBuilder.DropTable(
                name: "SubchIntros");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Jobs");
        }
    }
}
