using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monsters.backend.Migrations
{
    /// <inheritdoc />
    public partial class ComponentRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CCLinks");

            migrationBuilder.DropIndex(
                name: "IX_Components_Name",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "isComponent",
                table: "Components",
                newName: "isCraftingMaterial");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatureTypeId",
                table: "Components",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "DifficultyClass",
                table: "Components",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Components_CreatureTypeId",
                table: "Components",
                column: "CreatureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_CreatureTypes_CreatureTypeId",
                table: "Components",
                column: "CreatureTypeId",
                principalTable: "CreatureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_CreatureTypes_CreatureTypeId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_CreatureTypeId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "CreatureTypeId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "DifficultyClass",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "isCraftingMaterial",
                table: "Components",
                newName: "isComponent");

            migrationBuilder.CreateTable(
                name: "CCLinks",
                columns: table => new
                {
                    CreatureTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DifficultyClass = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CCLinks", x => new { x.CreatureTypeId, x.ComponentId });
                    table.ForeignKey(
                        name: "FK_CCLinks_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CCLinks_CreatureTypes_CreatureTypeId",
                        column: x => x.CreatureTypeId,
                        principalTable: "CreatureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_Name",
                table: "Components",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CCLinks_ComponentId",
                table: "CCLinks",
                column: "ComponentId");
        }
    }
}
