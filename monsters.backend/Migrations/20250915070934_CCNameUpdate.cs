using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monsters.backend.Migrations
{
    /// <inheritdoc />
    public partial class CCNameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CCLink_Components_ComponentId",
                table: "CCLink");

            migrationBuilder.DropForeignKey(
                name: "FK_CCLink_CreatureTypes_CreatureTypeId",
                table: "CCLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CCLink",
                table: "CCLink");

            migrationBuilder.RenameTable(
                name: "CCLink",
                newName: "CCLinks");

            migrationBuilder.RenameIndex(
                name: "IX_CCLink_ComponentId",
                table: "CCLinks",
                newName: "IX_CCLinks_ComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CCLinks",
                table: "CCLinks",
                columns: new[] { "CreatureTypeId", "ComponentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CCLinks_Components_ComponentId",
                table: "CCLinks",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CCLinks_CreatureTypes_CreatureTypeId",
                table: "CCLinks",
                column: "CreatureTypeId",
                principalTable: "CreatureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CCLinks_Components_ComponentId",
                table: "CCLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_CCLinks_CreatureTypes_CreatureTypeId",
                table: "CCLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CCLinks",
                table: "CCLinks");

            migrationBuilder.RenameTable(
                name: "CCLinks",
                newName: "CCLink");

            migrationBuilder.RenameIndex(
                name: "IX_CCLinks_ComponentId",
                table: "CCLink",
                newName: "IX_CCLink_ComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CCLink",
                table: "CCLink",
                columns: new[] { "CreatureTypeId", "ComponentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CCLink_Components_ComponentId",
                table: "CCLink",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CCLink_CreatureTypes_CreatureTypeId",
                table: "CCLink",
                column: "CreatureTypeId",
                principalTable: "CreatureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
