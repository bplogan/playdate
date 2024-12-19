using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDate.Migrations
{
    /// <inheritdoc />
    public partial class event_functions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerEvents_Events_EventId",
                table: "PlayerEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerEvents_Players_PlayerId",
                table: "PlayerEvents");

            migrationBuilder.DropIndex(
                name: "IX_Events_PlayerId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "TavelTypeBack",
                table: "PlayerEvents",
                newName: "TravelTypeBack");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Events",
                newName: "HostPlayerId");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "PlayerEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EventId",
                table: "PlayerEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerEvents_Events_EventId",
                table: "PlayerEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerEvents_Players_PlayerId",
                table: "PlayerEvents",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerEvents_Events_EventId",
                table: "PlayerEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerEvents_Players_PlayerId",
                table: "PlayerEvents");

            migrationBuilder.RenameColumn(
                name: "TravelTypeBack",
                table: "PlayerEvents",
                newName: "TavelTypeBack");

            migrationBuilder.RenameColumn(
                name: "HostPlayerId",
                table: "Events",
                newName: "PlayerId");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "PlayerEvents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EventId",
                table: "PlayerEvents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PlayerId",
                table: "Events",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerEvents_Events_EventId",
                table: "PlayerEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerEvents_Players_PlayerId",
                table: "PlayerEvents",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
