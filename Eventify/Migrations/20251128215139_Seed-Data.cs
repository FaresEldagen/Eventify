using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eventify.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    BIO = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Country = table.Column<int>(type: "int", nullable: true),
                    FrontIdPhoto = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    BackIdPhoto = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    ArabicAddress = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    ArabicFullName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    JoinedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    NationalIDNumber = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    ExperienceYear = table.Column<int>(type: "INT", maxLength: 50, nullable: true),
                    PastEventCount = table.Column<int>(type: "INT", maxLength: 50, nullable: true),
                    Specialization = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    VenueCount = table.Column<int>(type: "INT", nullable: true),
                    WithdrawableEarnings = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    VenueType = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    ZIP = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(Max)", nullable: false),
                    Capacity = table.Column<int>(type: "INT", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    SpecialFeatures = table.Column<string>(type: "VARCHAR(Max)", nullable: false),
                    AirConditioningAvailable = table.Column<bool>(type: "BIT", nullable: false),
                    CateringAvailable = table.Column<bool>(type: "BIT", nullable: false),
                    WifiAvailable = table.Column<bool>(type: "BIT", nullable: false),
                    ParkingAvailable = table.Column<bool>(type: "BIT", nullable: false),
                    BarServiceAvailable = table.Column<bool>(type: "BIT", nullable: false),
                    RestroomsAvailable = table.Column<bool>(type: "BIT", nullable: false),
                    AudioVisualEquipment = table.Column<bool>(type: "BIT", nullable: false),
                    ProofOfOwnership = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    OwnerId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venues_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTitle = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    IsPrivate = table.Column<bool>(type: "BIT", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    Features = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    VenueId = table.Column<int>(type: "INT", nullable: true),
                    OrganizerId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VenuePhotos",
                columns: table => new
                {
                    PhotoUrl = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    VenueId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenuePhotos", x => new { x.PhotoUrl, x.VenueId });
                    table.ForeignKey(
                        name: "FK_VenuePhotos_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventPhotos",
                columns: table => new
                {
                    PhotoUrl = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    EventId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPhotos", x => new { x.PhotoUrl, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventPhotos_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    Reference = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Owner", "OWNER" },
                    { 2, null, "Organizer", "ORGANIZER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ArabicAddress", "ArabicFullName", "BIO", "BackIdPhoto", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FrontIdPhoto", "Gender", "JoinedDate", "LockoutEnabled", "LockoutEnd", "NationalIDNumber", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType", "VenueCount", "WithdrawableEarnings" },
                values: new object[,]
                {
                    { 1, 0, "القاهرة - مصر", "محمود سمير عبد الله", "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.", "~/image/back1.jpg", "ac9efdbc-78b5-477c-bbb4-12bb002728a7", 1, "Owner1@test.com", true, "~/image/front1.jpg", 1, new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "29701020123455", "OWNER1@TEST.COM", "MAHMOUD", "AQAAAAIAAYagAAAAEOdKBfjp2SVAHCiiePOhhTRjtkaeFeoVW5QJ23PvxwIJuB6Rmx41xxh5R9s+gi/WLQ==", null, false, "~/image/avatar.jpg", "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54", false, "Mahmoud", "Owner", 3, 15000.75m },
                    { 2, 0, "القاهرة - مصر", "محمود سمير عبد الله", "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.", "~/image/back1.jpg", "e28083bb-f5dd-4b2a-9cd2-53e34b6eea3b", 1, "Owner2@test.com", true, "~/image/front1.jpg", 1, new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "29701020123455", "OWNER2@TEST.COM", "ALI", "AQAAAAIAAYagAAAAENK6MlVqGiMX2jHGYz7wNSGq+7ucvLKoAqvwZKmDJlbezFKjSguSLoL2iNSFffBbCw==", null, false, "~/image/avatar.jpg", "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54", false, "Ali", "Owner", 3, 15000.75m },
                    { 3, 0, "القاهرة - مصر", "محمود سمير عبد الله", "Venue owner with a passion for hosting memorable experiences. With more than 5 years in the industry, I provide versatile spaces tailored for events of all kinds, ensuring clients and guests enjoy smooth and successful gatherings.", "~/image/back1.jpg", "3e787f0c-93f4-4307-bd3c-3bf4bfb11206", 1, "Owner3@test.com", true, "~/image/front1.jpg", 1, new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "29701020123455", "OWNER3@TEST.COM", "AMR", "AQAAAAIAAYagAAAAEOXZOmZLqAUlbhmU39CggMHHc0k+Yavt/k3OlfON4ChYK+fxto1z/qEFuhl733BJlg==", null, false, "~/image/avatar.jpg", "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54", false, "Amr", "Owner", 3, 15000.75m }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ArabicAddress", "ArabicFullName", "BIO", "BackIdPhoto", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "ExperienceYear", "FrontIdPhoto", "Gender", "JoinedDate", "LockoutEnabled", "LockoutEnd", "NationalIDNumber", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PastEventCount", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "SecurityStamp", "Specialization", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { 4, 0, "القاهرة - مصر", "فارس حسن علي الداجن", "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.", "~/image/back1.jpg", "1f28393b-f52a-4d31-a0a3-0801eda15518", 1, "Organizer1@test.com", true, 5, "~/image/front1.jpg", 1, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "29801150123456", "ORGANIZER1@TEST.COM", "FARES", "AQAAAAIAAYagAAAAEBv0HeE9n7HGuWT6CORPM26N2jIGxzfZRgmA+BcfSS12KFqJTzSfTMOt09Srg6ZxaA==", 12, null, false, "~/image/avatar.jpg", "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54", "Tech Events", false, "Fares", "Organizier" },
                    { 5, 0, "القاهرة - مصر", "فارس حسن علي الداجن", "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.", "~/image/back1.jpg", "ca0049c9-cdc6-4781-b2cf-c290e2c1f86e", 1, "Organizer2@test.com", true, 5, "~/image/front1.jpg", 1, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "29801150123456", "ORGANIZER2@TEST.COM", "AHMED", "AQAAAAIAAYagAAAAEILcLJ6Zuil/RlAwcXdz9VbF0ekyusoNJXFfyz41rqHApzQy/xAw+3nA+KlOJlr6PQ==", 12, null, false, "~/image/avatar.jpg", "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54", "Tech Events", false, "Ahmed", "Organizier" },
                    { 6, 0, "القاهرة - مصر", "فارس حسن علي الداجن", "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.", "~/image/back1.jpg", "b6784ed2-50d7-4f24-be36-fb6fa7454e90", 1, "Organizer3@test.com", true, 5, "~/image/front1.jpg", 1, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "29801150123456", "ORGANIZER3@TEST.COM", "ZIAD", "AQAAAAIAAYagAAAAEP/X0PBUJ7tnzeqW7jN+kXs2vPl4D/EZGec0TAMpB7ukZv3O3mZkCw56UTWLBRAGNg==", 12, null, false, "~/image/avatar.jpg", "F2821D12 - 02EC - 4EB6 - 88A7 - 393BBAAD4D54", "Tech Events", false, "Ziad", "Organizier" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "AirConditioningAvailable", "AudioVisualEquipment", "BarServiceAvailable", "Capacity", "CateringAvailable", "City", "Description", "Name", "OwnerId", "ParkingAvailable", "PricePerHour", "ProofOfOwnership", "RestroomsAvailable", "SpecialFeatures", "State", "VenueType", "WifiAvailable", "ZIP" },
                values: new object[,]
                {
                    { 1, "Nasr City", true, true, false, 500, true, "Cairo", "Large premium indoor venue.", "Cairo Grand Hall", 1, true, 2500m, "~/images/ownership1.jpg", true, "Stage, LED Screens", "Cairo", 1, true, "11371" },
                    { 2, "Marina Walk", false, false, true, 300, true, "Dubai", "Open-air event arena with sea view.", "Dubai Outdoor Arena", 2, true, 1800m, "~/images/ownership1.jpg", true, "Sea View", "Dubai", 2, true, "00000" },
                    { 3, "Olaya Street", true, true, false, 700, false, "Riyadh", "Modern multi-purpose venue.", "Riyadh Event Center", 3, true, 3000m, "~/images/ownership1.jpg", true, "VIP Rooms", "Riyadh", 1, true, "11564" },
                    { 4, "Corniche Road", false, false, false, 250, true, "Alexandria", "Outdoor venue with stunning sea view.", "Alexandria Seaside Hall", 1, false, 1500m, "~/images/ownership1.jpg", true, "Sea Breeze, Open Stage", "Alexandria", 2, true, "21500" },
                    { 5, "Pyramids Road", false, false, false, 600, true, "Giza", "Cultural event venue facing pyramids.", "Giza Pyramid Arena", 1, true, 3500m, "~/images/ownership1.jpg", true, "Historic View", "Giza", 2, false, "12556" },
                    { 6, "Khalifa Street", true, true, true, 450, true, "Abu Dhabi", "Luxury indoor venue for premium events.", "Abu Dhabi Royal Hall", 2, true, 4000m, "~/images/ownership1.jpg", true, "Gold Interior, VIP Lounge", "Abu Dhabi", 1, true, "00001" },
                    { 7, "North Corniche", false, false, true, 800, false, "Jeddah", "Outdoor beach concert venue.", "Jeddah Beach Stage", 3, true, 2800m, "~/images/ownership1.jpg", true, "Beachfront Stage", "Mecca", 2, true, "23415" },
                    { 8, "West Bay", true, true, false, 1000, false, "Doha", "Large conference and exhibition hall.", "Doha Convention Hall", 2, true, 5000m, "~/images/ownership1.jpg", true, "Conference Rooms", "Doha", 1, true, "00022" },
                    { 9, "Luxor Temple Road", true, false, false, 350, false, "Luxor", "Historic indoor cultural venue.", "Luxor Cultural Theatre", 1, true, 1700m, "~/images/ownership1.jpg", true, "Theatre Stage", "Luxor", 1, false, "85958" },
                    { 10, "Art District", true, false, false, 120, false, "Sharjah", "Gallery space for exhibitions.", "Sharjah Art Gallery", 2, true, 900m, "~/images/ownership1.jpg", true, "Art Lighting", "Sharjah", 1, true, "00033" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Address", "Category", "Description", "EndDateTime", "EventTitle", "Features", "IsPrivate", "OrganizerId", "StartDateTime", "Status", "TicketPrice", "VenueId" },
                values: new object[,]
                {
                    { 1, "Cairo Grand Hall, Nasr City", 1, "A conference discussing the future of AI and technology.", new DateTime(2025, 4, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), "Tech Innovations Conference 2025", "Speakers, Workshops, Networking", false, 4, new DateTime(2025, 4, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, 500m, 1 },
                    { 2, "Dubai Outdoor Arena, Marina Walk", 8, "Intense fitness session with professional trainers.", new DateTime(2025, 5, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), "Outdoor Fitness Bootcamp", "Trainers, Fresh Air, Group Activities", false, 5, new DateTime(2025, 5, 3, 7, 0, 0, 0, DateTimeKind.Unspecified), 1, 150m, 2 },
                    { 3, "Riyadh Event Center", 5, "Connect with entrepreneurs and business owners.", new DateTime(2025, 6, 18, 23, 0, 0, 0, DateTimeKind.Unspecified), "Riyadh Business Networking Night", "Networking, Snacks, Business Talks", false, 6, new DateTime(2025, 6, 18, 19, 0, 0, 0, DateTimeKind.Unspecified), 4, 300m, 3 },
                    { 4, "Corniche Road", 10, "Live music performances by local bands.", new DateTime(2025, 7, 10, 23, 0, 0, 0, DateTimeKind.Unspecified), "Alexandria Summer Music Festival", "Live Bands, Food Trucks, Sea View", false, 4, new DateTime(2025, 7, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), 5, 200m, 4 },
                    { 5, "Pyramids Road", 3, "A meetup focusing on ancient Egyptian culture.", new DateTime(2025, 3, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Giza Cultural Meetup", "Guided Tour, Cultural Talks", true, 5, new DateTime(2025, 3, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), 3, 250m, 5 },
                    { 6, "Khalifa Street", 6, "A premium exhibition showcasing luxury brands.", new DateTime(2025, 9, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), "Abu Dhabi Luxury Expo", "Exhibitions, VIP Lounge", false, 6, new DateTime(2025, 9, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 5, 800m, 6 },
                    { 7, "Jeddah North Corniche", 9, "A charity sports event to support children’s hospitals.", new DateTime(2025, 2, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), "Jeddah Beach Charity Run", "Medals, Refreshments", false, 4, new DateTime(2025, 2, 20, 6, 0, 0, 0, DateTimeKind.Unspecified), 5, 100m, 7 },
                    { 8, "Doha Convention Hall", 2, "Hands-on workshop for beginners in software development.", new DateTime(2025, 8, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), "Doha Tech Workshop", "Coding Session, Mentors", false, 5, new DateTime(2025, 8, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, 350m, 8 },
                    { 9, "Luxor Temple Road", 4, "A seminar discussing ancient Egyptian heritage.", new DateTime(2025, 11, 2, 19, 0, 0, 0, DateTimeKind.Unspecified), "Luxor Historical Seminar", "Speakers, Guided Discussion", false, 6, new DateTime(2025, 11, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, 180m, 9 },
                    { 10, "Sharjah Art Gallery", 10, "A digital art exhibition featuring creatives from the region.", new DateTime(2025, 10, 10, 20, 0, 0, 0, DateTimeKind.Unspecified), "Sharjah Digital Art Expo", "Digital Art Panels, Artist Meetups", false, 4, new DateTime(2025, 10, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 5, 220m, 10 },
                    { 11, "Pyramids Road", 3, "A meetup focusing on ancient Egyptian culture.", new DateTime(2025, 3, 6, 18, 0, 0, 0, DateTimeKind.Unspecified), "Giza Cultural Meetup", "Guided Tour, Cultural Talks", false, 5, new DateTime(2025, 3, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, 250m, 5 }
                });

            migrationBuilder.InsertData(
                table: "VenuePhotos",
                columns: new[] { "PhotoUrl", "VenueId" },
                values: new object[,]
                {
                    { "~/images/venue1_1.jpg", 1 },
                    { "~/images/venue1_1.jpg", 2 },
                    { "~/images/venue1_1.jpg", 3 },
                    { "~/images/venue1_1.jpg", 4 },
                    { "~/images/venue1_1.jpg", 5 },
                    { "~/images/venue1_1.jpg", 6 },
                    { "~/images/venue1_1.jpg", 7 },
                    { "~/images/venue1_1.jpg", 8 },
                    { "~/images/venue1_1.jpg", 9 },
                    { "~/images/venue1_1.jpg", 10 },
                    { "~/images/venue1_2.jpg", 1 },
                    { "~/images/venue1_2.jpg", 2 },
                    { "~/images/venue1_2.jpg", 3 },
                    { "~/images/venue1_2.jpg", 4 },
                    { "~/images/venue1_2.jpg", 5 },
                    { "~/images/venue1_2.jpg", 6 },
                    { "~/images/venue1_2.jpg", 7 },
                    { "~/images/venue1_2.jpg", 8 },
                    { "~/images/venue1_2.jpg", 9 },
                    { "~/images/venue1_2.jpg", 10 },
                    { "~/images/venue1_3.jpg", 1 },
                    { "~/images/venue1_3.jpg", 2 },
                    { "~/images/venue1_3.jpg", 3 },
                    { "~/images/venue1_3.jpg", 4 },
                    { "~/images/venue1_3.jpg", 5 },
                    { "~/images/venue1_3.jpg", 6 },
                    { "~/images/venue1_3.jpg", 7 },
                    { "~/images/venue1_3.jpg", 8 },
                    { "~/images/venue1_3.jpg", 9 },
                    { "~/images/venue1_3.jpg", 10 },
                    { "~/images/venue1_4.jpg", 1 },
                    { "~/images/venue1_4.jpg", 2 },
                    { "~/images/venue1_4.jpg", 3 },
                    { "~/images/venue1_4.jpg", 4 },
                    { "~/images/venue1_4.jpg", 5 },
                    { "~/images/venue1_4.jpg", 6 },
                    { "~/images/venue1_4.jpg", 7 },
                    { "~/images/venue1_4.jpg", 8 },
                    { "~/images/venue1_4.jpg", 9 },
                    { "~/images/venue1_4.jpg", 10 }
                });

            migrationBuilder.InsertData(
                table: "EventPhotos",
                columns: new[] { "EventId", "PhotoUrl" },
                values: new object[,]
                {
                    { 1, "~/images/event1_1.jpg" },
                    { 2, "~/images/event1_1.jpg" },
                    { 3, "~/images/event1_1.jpg" },
                    { 4, "~/images/event1_1.jpg" },
                    { 5, "~/images/event1_1.jpg" },
                    { 6, "~/images/event1_1.jpg" },
                    { 7, "~/images/event1_1.jpg" },
                    { 8, "~/images/event1_1.jpg" },
                    { 9, "~/images/event1_1.jpg" },
                    { 10, "~/images/event1_1.jpg" },
                    { 11, "~/images/event1_1.jpg" },
                    { 1, "~/images/event1_2.jpg" },
                    { 2, "~/images/event1_2.jpg" },
                    { 3, "~/images/event1_2.jpg" },
                    { 4, "~/images/event1_2.jpg" },
                    { 5, "~/images/event1_2.jpg" },
                    { 6, "~/images/event1_2.jpg" },
                    { 7, "~/images/event1_2.jpg" },
                    { 8, "~/images/event1_2.jpg" },
                    { 9, "~/images/event1_2.jpg" },
                    { 10, "~/images/event1_2.jpg" },
                    { 11, "~/images/event1_2.jpg" },
                    { 1, "~/images/event1_3.jpg" },
                    { 2, "~/images/event1_3.jpg" },
                    { 3, "~/images/event1_3.jpg" },
                    { 4, "~/images/event1_3.jpg" },
                    { 5, "~/images/event1_3.jpg" },
                    { 6, "~/images/event1_3.jpg" },
                    { 7, "~/images/event1_3.jpg" },
                    { 8, "~/images/event1_3.jpg" },
                    { 9, "~/images/event1_3.jpg" },
                    { 10, "~/images/event1_3.jpg" },
                    { 11, "~/images/event1_3.jpg" },
                    { 1, "~/images/event1_4.jpg" },
                    { 2, "~/images/event1_4.jpg" },
                    { 3, "~/images/event1_4.jpg" },
                    { 4, "~/images/event1_4.jpg" },
                    { 5, "~/images/event1_4.jpg" },
                    { 6, "~/images/event1_4.jpg" },
                    { 7, "~/images/event1_4.jpg" },
                    { 8, "~/images/event1_4.jpg" },
                    { 9, "~/images/event1_4.jpg" },
                    { 10, "~/images/event1_4.jpg" },
                    { 11, "~/images/event1_4.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "EventId", "EventName", "PaymentDate", "Reference", "status" },
                values: new object[,]
                {
                    { 4, 200m, 4, "Alexandria Summer Music Festival", new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAY-EVT4-001", 2 },
                    { 6, 800m, 6, "Abu Dhabi Luxury Expo", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAY-EVT6-001", 2 },
                    { 7, 100m, 7, "Jeddah Beach Charity Run", new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAY-EVT7-001", 2 },
                    { 8, 350m, 8, "Doha Tech Workshop", new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAY-EVT8-001", 2 },
                    { 10, 220m, 10, "Sharjah Digital Art Expo", new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAY-EVT10-001", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EventPhotos_EventId",
                table: "EventPhotos",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EventId",
                table: "Payments",
                column: "EventId",
                unique: true,
                filter: "[EventId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VenuePhotos_VenueId",
                table: "VenuePhotos",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_OwnerId",
                table: "Venues",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EventPhotos");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "VenuePhotos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
