using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BerrasBio.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaloonModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaloonModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalSeatNumber = table.Column<int>(type: "int", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    SaloonModelId = table.Column<int>(type: "int", nullable: false),
                    BookingModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatModels_SaloonModels_SaloonModelId",
                        column: x => x.SaloonModelId,
                        principalTable: "SaloonModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveMovieModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaloonModelId = table.Column<int>(type: "int", nullable: false),
                    MovieModelId = table.Column<int>(type: "int", nullable: false),
                    TimeModelId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveMovieModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveMovieModels_MovieModels_MovieModelId",
                        column: x => x.MovieModelId,
                        principalTable: "MovieModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveMovieModels_SaloonModels_SaloonModelId",
                        column: x => x.SaloonModelId,
                        principalTable: "SaloonModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveMovieModels_TimeModels_TimeModelId",
                        column: x => x.TimeModelId,
                        principalTable: "TimeModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveMovieModelId = table.Column<int>(type: "int", nullable: false),
                    BookedTickets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingModels_ActiveMovieModels_ActiveMovieModelId",
                        column: x => x.ActiveMovieModelId,
                        principalTable: "ActiveMovieModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveMovieModels_MovieModelId",
                table: "ActiveMovieModels",
                column: "MovieModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveMovieModels_SaloonModelId",
                table: "ActiveMovieModels",
                column: "SaloonModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveMovieModels_TimeModelId",
                table: "ActiveMovieModels",
                column: "TimeModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingModels_ActiveMovieModelId",
                table: "BookingModels",
                column: "ActiveMovieModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatModels_SaloonModelId",
                table: "SeatModels",
                column: "SaloonModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingModels");

            migrationBuilder.DropTable(
                name: "SeatModels");

            migrationBuilder.DropTable(
                name: "ActiveMovieModels");

            migrationBuilder.DropTable(
                name: "MovieModels");

            migrationBuilder.DropTable(
                name: "SaloonModels");

            migrationBuilder.DropTable(
                name: "TimeModels");
        }
    }
}
