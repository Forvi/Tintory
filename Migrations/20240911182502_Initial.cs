using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COLOR.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColorEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HexCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaletteEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaletteEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaletteColor",
                columns: table => new
                {
                    ColorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaletteId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaletteColor", x => new { x.ColorId, x.PaletteId });
                    table.ForeignKey(
                        name: "FK_PaletteColor_ColorEntities_ColorId",
                        column: x => x.ColorId,
                        principalTable: "ColorEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaletteColor_PaletteEntities_PaletteId",
                        column: x => x.PaletteId,
                        principalTable: "PaletteEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaletteColor_PaletteId",
                table: "PaletteColor",
                column: "PaletteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaletteColor");

            migrationBuilder.DropTable(
                name: "ColorEntities");

            migrationBuilder.DropTable(
                name: "PaletteEntities");
        }
    }
}
