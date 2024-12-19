using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDate.Migrations
{
    /// <inheritdoc />
    public partial class player_params : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCatAllergy",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDairyAllergy",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDogAllergy",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEggAllergy",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNutAllergy",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegan",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCatAllergy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsDairyAllergy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsDogAllergy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsEggAllergy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsNutAllergy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsVegan",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "Players");
        }
    }
}
