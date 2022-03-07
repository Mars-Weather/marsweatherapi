using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarsWeatherApi.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pressures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Average = table.Column<float>(type: "real", nullable: false),
                    Minimum = table.Column<float>(type: "real", nullable: false),
                    Maximum = table.Column<float>(type: "real", nullable: false),
                    SolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pressures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pressures_Sols_SolId",
                        column: x => x.SolId,
                        principalTable: "Sols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temperatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Average = table.Column<float>(type: "real", nullable: false),
                    Minimum = table.Column<float>(type: "real", nullable: false),
                    Maximum = table.Column<float>(type: "real", nullable: false),
                    SolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperatures_Sols_SolId",
                        column: x => x.SolId,
                        principalTable: "Sols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Winds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Average = table.Column<float>(type: "real", nullable: false),
                    Minimum = table.Column<float>(type: "real", nullable: false),
                    Maximum = table.Column<float>(type: "real", nullable: false),
                    MostCommonDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Winds_Sols_SolId",
                        column: x => x.SolId,
                        principalTable: "Sols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pressures_SolId",
                table: "Pressures",
                column: "SolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Temperatures_SolId",
                table: "Temperatures",
                column: "SolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Winds_SolId",
                table: "Winds",
                column: "SolId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pressures");

            migrationBuilder.DropTable(
                name: "Temperatures");

            migrationBuilder.DropTable(
                name: "Winds");

            migrationBuilder.DropTable(
                name: "Sols");
        }
    }
}
