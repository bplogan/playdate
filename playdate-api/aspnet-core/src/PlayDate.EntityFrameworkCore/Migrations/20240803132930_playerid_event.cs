using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDate.Migrations
{
    /// <inheritdoc />
    public partial class playerid_event : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "Events",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events");

            migrationBuilder.AlterColumn<long>(
                name: "PlayerId",
                table: "Events",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
