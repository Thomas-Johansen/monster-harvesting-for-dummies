using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monsters.backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreatureTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ChallengeRating = table.Column<int>(type: "integer", nullable: false),
                    AssociatedSkill = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CCLink",
                columns: table => new
                {
                    CreatureTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DifficultyClass = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CCLink", x => new { x.CreatureTypeId, x.ComponentId });
                    table.ForeignKey(
                        name: "FK_CCLink_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CCLink_CreatureTypes_CreatureTypeId",
                        column: x => x.CreatureTypeId,
                        principalTable: "CreatureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CCLink_ComponentId",
                table: "CCLink",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_Name",
                table: "Components",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreatureTypes_Name",
                table: "CreatureTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CCLink");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "CreatureTypes");
        }
    }
}
