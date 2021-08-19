using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarsLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSpeed = table.Column<double>(type: "float", nullable: false),
                    CarLength = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Speed = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Highways",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HighWayDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wheather = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highways", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ticketsList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketsList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarsInHighWay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<double>(type: "float", nullable: false),
                    LastLocationChangeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverID = table.Column<int>(type: "int", nullable: true),
                    HighwayId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsInHighWay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarsInHighWay_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarsInHighWay_Highways_HighwayId",
                        column: x => x.HighwayId,
                        principalTable: "Highways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpeedCameras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HighWayId = table.Column<int>(type: "int", nullable: false),
                    HighWayDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAllowedSpeed = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeedCameras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeedCameras_Highways_HighWayId",
                        column: x => x.HighWayId,
                        principalTable: "Highways",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    carsListId = table.Column<int>(type: "int", nullable: true),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarInHighwayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_CarsInHighWay_CarInHighwayId",
                        column: x => x.CarInHighwayId,
                        principalTable: "CarsInHighWay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_CarsLists_carsListId",
                        column: x => x.carsListId,
                        principalTable: "CarsLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cars_persons_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetectedCarsBySpeedCam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetectedPlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetectedSpeed = table.Column<double>(type: "float", nullable: false),
                    DetectTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpeedCameraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectedCarsBySpeedCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetectedCarsBySpeedCam_SpeedCameras_SpeedCameraId",
                        column: x => x.SpeedCameraId,
                        principalTable: "SpeedCameras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    TicketsListId = table.Column<int>(type: "int", nullable: true),
                    TicketDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_ticketsList_TicketsListId",
                        column: x => x.TicketsListId,
                        principalTable: "ticketsList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_CarInHighwayId",
                table: "cars",
                column: "CarInHighwayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cars_carsListId",
                table: "cars",
                column: "carsListId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_OwnerId",
                table: "cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsInHighWay_DriverID",
                table: "CarsInHighWay",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_CarsInHighWay_HighwayId",
                table: "CarsInHighWay",
                column: "HighwayId");

            migrationBuilder.CreateIndex(
                name: "IX_DetectedCarsBySpeedCam_SpeedCameraId",
                table: "DetectedCarsBySpeedCam",
                column: "SpeedCameraId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeedCameras_HighWayId",
                table: "SpeedCameras",
                column: "HighWayId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_CarId",
                table: "tickets",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_TicketsListId",
                table: "tickets",
                column: "TicketsListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetectedCarsBySpeedCam");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "SpeedCameras");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "ticketsList");

            migrationBuilder.DropTable(
                name: "CarsInHighWay");

            migrationBuilder.DropTable(
                name: "CarsLists");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Highways");
        }
    }
}
