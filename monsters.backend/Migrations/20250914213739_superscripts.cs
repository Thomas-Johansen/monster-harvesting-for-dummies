using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monsters.backend.Migrations
{
    /// <inheritdoc />
    public partial class superscripts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isComponent",
                table: "Components",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isEdible",
                table: "Components",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isComponent",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "isEdible",
                table: "Components");
        }
    }
}
