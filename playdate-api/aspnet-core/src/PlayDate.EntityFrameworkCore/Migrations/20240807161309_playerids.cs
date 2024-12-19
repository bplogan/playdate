using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDate.Migrations
{
    /// <inheritdoc />
    public partial class playerids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerAllergies_Players_PlayerId",
                table: "PlayerAllergies");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerInstructions_Players_PlayerId",
                table: "PlayerInstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerRestrictions_Players_PlayerId",
                table: "PlayerRestrictions");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "PlayerRestrictions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "PlayerInstructions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "PlayerAllergies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerAllergies_Players_PlayerId",
                table: "PlayerAllergies",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerInstructions_Players_PlayerId",
                table: "PlayerInstructions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerRestrictions_Players_PlayerId",
                table: "PlayerRestrictions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerAllergies_Players_PlayerId",
                table: "PlayerAllergies");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerInstructions_Players_PlayerId",
                table: "PlayerInstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerRestrictions_Players_PlayerId",
                table: "PlayerRestrictions");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "PlayerRestrictions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "PlayerInstructions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "PlayerAllergies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerAllergies_Players_PlayerId",
                table: "PlayerAllergies",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerInstructions_Players_PlayerId",
                table: "PlayerInstructions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerRestrictions_Players_PlayerId",
                table: "PlayerRestrictions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
