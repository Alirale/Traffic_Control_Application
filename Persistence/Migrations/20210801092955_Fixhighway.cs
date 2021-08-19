using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Fixhighway : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighWayDirection",
                table: "SpeedCameras");

            migrationBuilder.DropColumn(
                name: "MaxAllowedSpeed",
                table: "SpeedCameras");

            migrationBuilder.AddColumn<double>(
                name: "MaxAllowedSpeed",
                table: "Highways",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxAllowedSpeed",
                table: "Highways");

            migrationBuilder.AddColumn<string>(
                name: "HighWayDirection",
                table: "SpeedCameras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaxAllowedSpeed",
                table: "SpeedCameras",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
