﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseLayer.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    DestinationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Longitude = table.Column<decimal>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.DestinationId);
                });

            migrationBuilder.CreateTable(
                name: "FlightLuggage",
                columns: table => new
                {
                    FlightLuggageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlightLuggageType = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightLuggage", x => x.FlightLuggageId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Key = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<long>(nullable: false),
                    PassportId = table.Column<long>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    TokenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    AirlineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdministratorUserId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.AirlineId);
                    table.ForeignKey(
                        name: "FK_Airline_User_AdministratorUserId",
                        column: x => x.AdministratorUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    FriendshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FriendOfId = table.Column<int>(nullable: false),
                    FriendWithId = table.Column<int>(nullable: false),
                    Confirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.FriendshipId);
                    table.ForeignKey(
                        name: "FK_Friend_User_FriendOfId",
                        column: x => x.FriendOfId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friend_User_FriendWithId",
                        column: x => x.FriendWithId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AirlineDestination",
                columns: table => new
                {
                    AirlineId = table.Column<int>(nullable: false),
                    DestinationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineDestination", x => new { x.AirlineId, x.DestinationId });
                    table.ForeignKey(
                        name: "FK_AirlineDestination_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirlineDestination_Destination_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AirlineFlightLuggage",
                columns: table => new
                {
                    FlightLuggageId = table.Column<int>(nullable: false),
                    AirlineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirlineFlightLuggage", x => new { x.AirlineId, x.FlightLuggageId });
                    table.ForeignKey(
                        name: "FK_AirlineFlightLuggage_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirlineFlightLuggage_FlightLuggage_FlightLuggageId",
                        column: x => x.FlightLuggageId,
                        principalTable: "FlightLuggage",
                        principalColumn: "FlightLuggageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ToDestinationDestinationId = table.Column<int>(nullable: false),
                    FromDestinationDestinationId = table.Column<int>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    ArrivalDate = table.Column<DateTime>(nullable: false),
                    TripLength = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    NumOfChangeovers = table.Column<int>(nullable: false),
                    AirlineId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flight_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flight_Destination_FromDestinationDestinationId",
                        column: x => x.FromDestinationDestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flight_Destination_ToDestinationDestinationId",
                        column: x => x.ToDestinationDestinationId,
                        principalTable: "Destination",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightTicket",
                columns: table => new
                {
                    FlightTicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    FlightId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightTicket", x => x.FlightTicketId);
                    table.ForeignKey(
                        name: "FK_FlightTicket_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    SeatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SeatNumber = table.Column<int>(nullable: false),
                    SeatState = table.Column<int>(nullable: false),
                    FlightId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seat_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightOrder",
                columns: table => new
                {
                    FlightOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlightId = table.Column<int>(nullable: true),
                    FlightTicketId = table.Column<int>(nullable: true),
                    SeatId = table.Column<int>(nullable: true),
                    FlightLuggageId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    Confirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightOrder", x => x.FlightOrderId);
                    table.ForeignKey(
                        name: "FK_FlightOrder_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightOrder_FlightLuggage_FlightLuggageId",
                        column: x => x.FlightLuggageId,
                        principalTable: "FlightLuggage",
                        principalColumn: "FlightLuggageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightOrder_FlightTicket_FlightTicketId",
                        column: x => x.FlightTicketId,
                        principalTable: "FlightTicket",
                        principalColumn: "FlightTicketId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightOrder_Seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seat",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightOrder_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airline_AdministratorUserId",
                table: "Airline",
                column: "AdministratorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineDestination_DestinationId",
                table: "AirlineDestination",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_AirlineFlightLuggage_FlightLuggageId",
                table: "AirlineFlightLuggage",
                column: "FlightLuggageId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineId",
                table: "Flight",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_FromDestinationDestinationId",
                table: "Flight",
                column: "FromDestinationDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_ToDestinationDestinationId",
                table: "Flight",
                column: "ToDestinationDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightOrder_FlightId",
                table: "FlightOrder",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightOrder_FlightLuggageId",
                table: "FlightOrder",
                column: "FlightLuggageId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightOrder_FlightTicketId",
                table: "FlightOrder",
                column: "FlightTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightOrder_SeatId",
                table: "FlightOrder",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightOrder_UserId",
                table: "FlightOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightTicket_FlightId",
                table: "FlightTicket",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FriendOfId",
                table: "Friend",
                column: "FriendOfId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FriendWithId",
                table: "Friend",
                column: "FriendWithId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_FlightId",
                table: "Seat",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirlineDestination");

            migrationBuilder.DropTable(
                name: "AirlineFlightLuggage");

            migrationBuilder.DropTable(
                name: "FlightOrder");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "FlightLuggage");

            migrationBuilder.DropTable(
                name: "FlightTicket");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Airline");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
