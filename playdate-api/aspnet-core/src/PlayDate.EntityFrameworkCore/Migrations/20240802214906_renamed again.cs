using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDate.Migrations
{
    /// <inheritdoc />
    public partial class renamedagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerAllergies_AbpUsers_UserId",
                table: "PlayerAllergies");

            migrationBuilder.DropTable(
                name: "PlayerDetail");

            migrationBuilder.DropTable(
                name: "UserEvents");

            migrationBuilder.DropTable(
                name: "UserInstructions");

            migrationBuilder.DropTable(
                name: "UserRestrictions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PlayerAllergies",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerAllergies_UserId",
                table: "PlayerAllergies",
                newName: "IX_PlayerAllergies_PlayerId");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    City = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    IsSwimmer = table.Column<bool>(type: "bit", nullable: false),
                    IsPetAllergy = table.Column<bool>(type: "bit", nullable: false),
                    IsFoodRestricted = table.Column<bool>(type: "bit", nullable: false),
                    IsFoodAllergy = table.Column<bool>(type: "bit", nullable: false),
                    IsOtherAllergy = table.Column<bool>(type: "bit", nullable: false),
                    IsOtherRestricted = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecialInstructions = table.Column<bool>(type: "bit", nullable: false),
                    HasEpiPen = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: true),
                    EventId = table.Column<long>(type: "bigint", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TravelTypeThere = table.Column<int>(type: "int", nullable: false),
                    TavelTypeBack = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerEvents_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerInstructions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerInstructions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerRestrictions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RestrictionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRestrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRestrictions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerEvents_EventId",
                table: "PlayerEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerEvents_PlayerId",
                table: "PlayerEvents",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInstructions_PlayerId",
                table: "PlayerInstructions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRestrictions_PlayerId",
                table: "PlayerRestrictions",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerAllergies_Players_PlayerId",
                table: "PlayerAllergies",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerAllergies_Players_PlayerId",
                table: "PlayerAllergies");

            migrationBuilder.DropTable(
                name: "PlayerEvents");

            migrationBuilder.DropTable(
                name: "PlayerInstructions");

            migrationBuilder.DropTable(
                name: "PlayerRestrictions");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "PlayerAllergies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerAllergies_PlayerId",
                table: "PlayerAllergies",
                newName: "IX_PlayerAllergies_UserId");

            migrationBuilder.CreateTable(
                name: "PlayerDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasEpiPen = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsFoodAllergy = table.Column<bool>(type: "bit", nullable: false),
                    IsFoodRestricted = table.Column<bool>(type: "bit", nullable: false),
                    IsOtherAllergy = table.Column<bool>(type: "bit", nullable: false),
                    IsOtherRestricted = table.Column<bool>(type: "bit", nullable: false),
                    IsPetAllergy = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecialInstructions = table.Column<bool>(type: "bit", nullable: false),
                    IsSwimmer = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Province = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerDetail_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TavelTypeBack = table.Column<int>(type: "int", nullable: false),
                    TravelTypeThere = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEvents_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserInstructions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInstructions_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRestrictions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RestrictionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRestrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRestrictions_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDetail_UserId",
                table: "PlayerDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_UserId",
                table: "UserEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInstructions_UserId",
                table: "UserInstructions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRestrictions_UserId",
                table: "UserRestrictions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerAllergies_AbpUsers_UserId",
                table: "PlayerAllergies",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
