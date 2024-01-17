using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weekly_Weather.Migrations
{
    /// <inheritdoc />
    public partial class Migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Forecast",
                columns: table => new
                {
                    ForecastId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    temperature_2m_max = table.Column<float>(type: "real", nullable: false),
                    temperature_2m_min = table.Column<float>(type: "real", nullable: false),
                    sunrise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sunset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precipitation_sum = table.Column<float>(type: "real", nullable: false),
                    precipitation_probability_max = table.Column<float>(type: "real", nullable: false),
                    precipitation_sum_units = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    temperature_2m_units = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecast", x => x.ForecastId);
                    table.ForeignKey(
                        name: "FK_Forecast_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forecast_LocationId",
                table: "Forecast",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forecast");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
