using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetectedPlateNumber",
                table: "DetectedCarsBySpeedCam");

            migrationBuilder.DropColumn(
                name: "DetectedSpeed",
                table: "DetectedCarsBySpeedCam");

            migrationBuilder.DropColumn(
                name: "PlateNumber",
                table: "CarsInHighWay");

            migrationBuilder.AddColumn<int>(
                name: "DetectedCarId",
                table: "DetectedCarsBySpeedCam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetectedCarsBySpeedCam_DetectedCarId",
                table: "DetectedCarsBySpeedCam",
                column: "DetectedCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetectedCarsBySpeedCam_cars_DetectedCarId",
                table: "DetectedCarsBySpeedCam",
                column: "DetectedCarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetectedCarsBySpeedCam_cars_DetectedCarId",
                table: "DetectedCarsBySpeedCam");

            migrationBuilder.DropIndex(
                name: "IX_DetectedCarsBySpeedCam_DetectedCarId",
                table: "DetectedCarsBySpeedCam");

            migrationBuilder.DropColumn(
                name: "DetectedCarId",
                table: "DetectedCarsBySpeedCam");

            migrationBuilder.AddColumn<string>(
                name: "DetectedPlateNumber",
                table: "DetectedCarsBySpeedCam",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DetectedSpeed",
                table: "DetectedCarsBySpeedCam",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PlateNumber",
                table: "CarsInHighWay",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
