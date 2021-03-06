using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
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
                    Wheather = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAllowedSpeed = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highways", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ticketsLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketsLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeedCameras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HighWayId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<double>(type: "float", nullable: false)
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
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_persons_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    carsListId = table.Column<int>(type: "int", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_CarsLists_carsListId",
                        column: x => x.carsListId,
                        principalTable: "CarsLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_persons_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenExp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tokens_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarsInHighWay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        name: "FK_CarsInHighWay_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "DetectedCarsBySpeedCam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetectedCarId = table.Column<int>(type: "int", nullable: false),
                    DetectTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpeedCameraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectedCarsBySpeedCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetectedCarsBySpeedCam_cars_DetectedCarId",
                        column: x => x.DetectedCarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_tickets_ticketsLists_TicketsListId",
                        column: x => x.TicketsListId,
                        principalTable: "ticketsLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CarsLists",
                columns: new[] { "Id", "CarLength", "MaxSpeed", "Name" },
                values: new object[,]
                {
                    { 1, 1.6000000000000001, 140.0, "Pride" },
                    { 2, 1.75, 160.0, "L90" },
                    { 3, 1.8, 180.0, "Sonata" },
                    { 4, 1.8, 200.0, "Sorento" }
                });

            migrationBuilder.InsertData(
                table: "Highways",
                columns: new[] { "Id", "HighWayDirection", "MaxAllowedSpeed", "Wheather" },
                values: new object[,]
                {
                    { 1, "North", 90.0, "Sunny" },
                    { 2, "South", 90.0, "Sunny" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Citizen" },
                    { 2, "Police" },
                    { 3, "SuperAdmin" }
                });

            migrationBuilder.InsertData(
                table: "ticketsLists",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 1, "UnauthorizedSpeed", 200000f });

            migrationBuilder.InsertData(
                table: "persons",
                columns: new[] { "Id", "Name", "PasswordHash", "RoleId" },
                values: new object[,]
                {
                    { 2, "Mohamad", null, 1 },
                    { 3, "Abbas", null, 1 },
                    { 4, "Reza", null, 1 },
                    { 1, "Alireza", null, 3 }
                });

            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "Id", "OwnerId", "PlateNumber", "carsListId" },
                values: new object[,]
                {
                    { 3, 2, "59J973", 2 },
                    { 4, 3, "16T781", 3 },
                    { 5, 4, "87G725", 4 },
                    { 1, 1, "64P712", 1 },
                    { 2, 1, "87G725", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_carsListId",
                table: "cars",
                column: "carsListId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_OwnerId",
                table: "cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsInHighWay_CarId",
                table: "CarsInHighWay",
                column: "CarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarsInHighWay_DriverID",
                table: "CarsInHighWay",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_CarsInHighWay_HighwayId",
                table: "CarsInHighWay",
                column: "HighwayId");

            migrationBuilder.CreateIndex(
                name: "IX_DetectedCarsBySpeedCam_DetectedCarId",
                table: "DetectedCarsBySpeedCam",
                column: "DetectedCarId");

            migrationBuilder.CreateIndex(
                name: "IX_DetectedCarsBySpeedCam_SpeedCameraId",
                table: "DetectedCarsBySpeedCam",
                column: "SpeedCameraId");

            migrationBuilder.CreateIndex(
                name: "IX_persons_RoleId",
                table: "persons",
                column: "RoleId");

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

            migrationBuilder.CreateIndex(
                name: "IX_tokens_PersonId",
                table: "tokens",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarsInHighWay");

            migrationBuilder.DropTable(
                name: "DetectedCarsBySpeedCam");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "tokens");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "SpeedCameras");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "ticketsLists");

            migrationBuilder.DropTable(
                name: "Highways");

            migrationBuilder.DropTable(
                name: "CarsLists");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
