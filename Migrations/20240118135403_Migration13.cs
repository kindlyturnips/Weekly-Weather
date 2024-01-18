using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weekly_Weather.Migrations
{
    /// <inheritdoc />
    public partial class Migration13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "weather_code",
                table: "Forecast",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "weather_code",
                table: "Forecast");
        }
    }
}
