using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDate.Migrations
{
    /// <inheritdoc />
    public partial class event_player : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PlayerId",
                table: "Events",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_PlayerId",
                table: "Events",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_PlayerId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_PlayerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Events");
        }
    }
}
