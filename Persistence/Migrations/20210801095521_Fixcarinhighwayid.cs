using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Fixcarinhighwayid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_CarsInHighWay_CarInHighwayId",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "IX_cars_CarInHighwayId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "CarInHighwayId",
                table: "cars");

            migrationBuilder.CreateIndex(
                name: "IX_CarsInHighWay_CarId",
                table: "CarsInHighWay",
                column: "CarId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsInHighWay_cars_CarId",
                table: "CarsInHighWay",
                column: "CarId",
                principalTable: "cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsInHighWay_cars_CarId",
                table: "CarsInHighWay");

            migrationBuilder.DropIndex(
                name: "IX_CarsInHighWay_CarId",
                table: "CarsInHighWay");

            migrationBuilder.AddColumn<int>(
                name: "CarInHighwayId",
                table: "cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cars_CarInHighwayId",
                table: "cars",
                column: "CarInHighwayId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cars_CarsInHighWay_CarInHighwayId",
                table: "cars",
                column: "CarInHighwayId",
                principalTable: "CarsInHighWay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
