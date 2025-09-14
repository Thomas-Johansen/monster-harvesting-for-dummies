using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monsters.backend.Migrations
{
    /// <inheritdoc />
    public partial class ProperCreatureTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChallengeRating",
                table: "CreatureTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChallengeRating",
                table: "CreatureTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
