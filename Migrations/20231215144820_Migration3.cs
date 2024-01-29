using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weekly_Weather.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country_code",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "county",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "display_name",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lat",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lon",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "country_code",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "county",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "display_name",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "state",
                table: "Location");
        }
    }
}
