﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211019083857_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entities.Background.CarInHighway", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int?>("DriverID")
                        .HasColumnType("int");

                    b.Property<int>("HighwayId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastLocationChangeTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Location")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.HasIndex("DriverID");

                    b.HasIndex("HighwayId");

                    b.ToTable("CarsInHighWay");
                });

            modelBuilder.Entity("Core.Entities.Background.DetectedCarBySpeedCam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DetectTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DetectedCarId")
                        .HasColumnType("int");

                    b.Property<int?>("SpeedCameraId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DetectedCarId");

                    b.HasIndex("SpeedCameraId");

                    b.ToTable("DetectedCarsBySpeedCam");
                });

            modelBuilder.Entity("Core.Entities.Background.Driver", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Speed")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Core.Entities.Background.Highway", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HighWayDirection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MaxAllowedSpeed")
                        .HasColumnType("float");

                    b.Property<string>("Wheather")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Highways");
                });

            modelBuilder.Entity("Core.Entities.Background.SpeedCamera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HighWayId")
                        .HasColumnType("int");

                    b.Property<double>("Location")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("HighWayId");

                    b.ToTable("SpeedCameras");
                });

            modelBuilder.Entity("Core.Entities.Police.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("carsListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("carsListId");

                    b.ToTable("cars");
                });

            modelBuilder.Entity("Core.Entities.Police.CarsList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CarLength")
                        .HasColumnType("float");

                    b.Property<double>("MaxSpeed")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarsLists");
                });

            modelBuilder.Entity("Core.Entities.Police.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("persons");
                });

            modelBuilder.Entity("Core.Entities.Police.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TicketDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TicketsListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("TicketsListId");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("Core.Entities.Police.TicketsList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ticketsLists");
                });

            modelBuilder.Entity("Core.Entities.Background.CarInHighway", b =>
                {
                    b.HasOne("Core.Entities.Police.Car", "Car")
                        .WithOne("CarInHighway")
                        .HasForeignKey("Core.Entities.Background.CarInHighway", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Background.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverID");

                    b.HasOne("Core.Entities.Background.Highway", "Highway")
                        .WithMany("CarInHighWays")
                        .HasForeignKey("HighwayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Driver");

                    b.Navigation("Highway");
                });

            modelBuilder.Entity("Core.Entities.Background.DetectedCarBySpeedCam", b =>
                {
                    b.HasOne("Core.Entities.Police.Car", "DetectedCar")
                        .WithMany()
                        .HasForeignKey("DetectedCarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Background.SpeedCamera", "SpeedCamera")
                        .WithMany("DetectedCars")
                        .HasForeignKey("SpeedCameraId");

                    b.Navigation("DetectedCar");

                    b.Navigation("SpeedCamera");
                });

            modelBuilder.Entity("Core.Entities.Background.SpeedCamera", b =>
                {
                    b.HasOne("Core.Entities.Background.Highway", "Highway")
                        .WithMany("SpeedCameras")
                        .HasForeignKey("HighWayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Highway");
                });

            modelBuilder.Entity("Core.Entities.Police.Car", b =>
                {
                    b.HasOne("Core.Entities.Police.Person", "Owner")
                        .WithMany("Cars")
                        .HasForeignKey("OwnerId");

                    b.HasOne("Core.Entities.Police.CarsList", "carsList")
                        .WithMany("Cars")
                        .HasForeignKey("carsListId");

                    b.Navigation("carsList");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Core.Entities.Police.Ticket", b =>
                {
                    b.HasOne("Core.Entities.Police.Car", "Car")
                        .WithMany("Tickets")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Police.TicketsList", "TicketsList")
                        .WithMany("Tickets")
                        .HasForeignKey("TicketsListId");

                    b.Navigation("Car");

                    b.Navigation("TicketsList");
                });

            modelBuilder.Entity("Core.Entities.Background.Highway", b =>
                {
                    b.Navigation("CarInHighWays");

                    b.Navigation("SpeedCameras");
                });

            modelBuilder.Entity("Core.Entities.Background.SpeedCamera", b =>
                {
                    b.Navigation("DetectedCars");
                });

            modelBuilder.Entity("Core.Entities.Police.Car", b =>
                {
                    b.Navigation("CarInHighway");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Core.Entities.Police.CarsList", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("Core.Entities.Police.Person", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("Core.Entities.Police.TicketsList", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
