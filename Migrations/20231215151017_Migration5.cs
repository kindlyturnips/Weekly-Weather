using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weekly_Weather.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Location",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Location",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Lon",
                table: "Location",
                newName: "lon");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "Location",
                newName: "lat");

            migrationBuilder.RenameColumn(
                name: "County",
                table: "Location",
                newName: "county");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Location",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Location",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "Location",
                newName: "display_name");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Location",
                newName: "country_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "state",
                table: "Location",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Location",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "lon",
                table: "Location",
                newName: "Lon");

            migrationBuilder.RenameColumn(
                name: "lat",
                table: "Location",
                newName: "Lat");

            migrationBuilder.RenameColumn(
                name: "county",
                table: "Location",
                newName: "County");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Location",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Location",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "display_name",
                table: "Location",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "country_code",
                table: "Location",
                newName: "CountryCode");
        }
    }
}
