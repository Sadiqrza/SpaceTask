using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceTask.Data.Migrations
{
    public partial class SpaceTaskDALSpaceTaskDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Watchlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MovieId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendingMailTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsWatched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Watchlists_UserId_MovieName",
                table: "Watchlists",
                columns: new[] { "UserId", "MovieName" },
                unique: true,
                filter: "[MovieName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Watchlists");
        }
    }
}
