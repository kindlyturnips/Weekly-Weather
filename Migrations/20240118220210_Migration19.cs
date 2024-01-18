using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weekly_Weather.Migrations
{
    /// <inheritdoc />
    public partial class Migration19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forecast_Location_LocationId",
                table: "Forecast");

            migrationBuilder.DropForeignKey(
                name: "FK_Forecast_Location_LocationId1",
                table: "Forecast");

            migrationBuilder.DropIndex(
                name: "IX_Forecast_LocationId",
                table: "Forecast");

            migrationBuilder.DropIndex(
                name: "IX_Forecast_LocationId1",
                table: "Forecast");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Forecast");

            migrationBuilder.CreateIndex(
                name: "IX_Forecast_LocationId",
                table: "Forecast",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forecast_Location_LocationId",
                table: "Forecast",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forecast_Location_LocationId",
                table: "Forecast");

            migrationBuilder.DropIndex(
                name: "IX_Forecast_LocationId",
                table: "Forecast");

            migrationBuilder.AddColumn<int>(
                name: "LocationId1",
                table: "Forecast",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forecast_LocationId",
                table: "Forecast",
                column: "LocationId",
                unique: true,
                filter: "[LocationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Forecast_LocationId1",
                table: "Forecast",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Forecast_Location_LocationId",
                table: "Forecast",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forecast_Location_LocationId1",
                table: "Forecast",
                column: "LocationId1",
                principalTable: "Location",
                principalColumn: "LocationId");
        }
    }
}
