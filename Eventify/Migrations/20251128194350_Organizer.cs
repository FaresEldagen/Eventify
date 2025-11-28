using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eventify.Migrations
{
    /// <inheritdoc />
    public partial class Organizer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ArabicAddress", "ArabicFullName", "BIO", "BackIdPhoto", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "ExperienceYear", "FrontIdPhoto", "Gender", "JoinedDate", "LockoutEnabled", "LockoutEnd", "NationalIDNumber", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PastEventCount", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "SecurityStamp", "Specialization", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { 4, 0, "القاهرة - مصر", "فارس حسن علي الداجن", "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.", "~/image/back1.jpg", "d2ef2bc3-feb9-43df-a798-569418a51634", 1, "Organizer1@test.com", true, 5, "~/image/front1.jpg", 1, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), false, null, "29801150123456", "ORGANIZER1@TEST.COM", "FARES", "AQAAAAIAAYagAAAAEJBpTyyvJ0sHYPQ+NgQe5y3n2a1Zgxk2c9RuyHo7aUZPuBfVUY41k2K5HkTtbcX6Rw==", 12, null, false, "~/image/avatar.jpg", null, "Tech Events", false, "Fares", "Organizier" },
                    { 5, 0, "القاهرة - مصر", "فارس حسن علي الداجن", "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.", "~/image/back1.jpg", "4787fa46-b873-4598-95e4-dc973b3cf768", 1, "Organizer2@test.com", true, 5, "~/image/front1.jpg", 1, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), false, null, "29801150123456", "ORGANIZER2@TEST.COM", "AHMED", "AQAAAAIAAYagAAAAEH5RBCYP6r/JwuUGaSrt+i/xwcA3AQ8UDxRhq4HYBIN1vRIjguQK6pVVZ9Ge6xGdDQ==", 12, null, false, "~/image/avatar.jpg", null, "Tech Events", false, "Ahmed", "Organizier" },
                    { 6, 0, "القاهرة - مصر", "فارس حسن علي الداجن", "Event Organizer passionate about designing and managing unforgettable events. With over 5 years in the industry, I specialize in bringing ideas to life, coordinating every detail, and creating seamless experiences for attendees.", "~/image/back1.jpg", "f7a3df34-c2d3-49e9-9bd1-9b0846af6349", 1, "Organizer3@test.com", true, 5, "~/image/front1.jpg", 1, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), false, null, "29801150123456", "ORGANIZER3@TEST.COM", "ZIAD", "AQAAAAIAAYagAAAAEOQsgVdVfIpBwUuFRGflz4CfyxTIDJFC9wDziNk5DPFnKwCXH1d0M0mU7WLS0VPlSw==", 12, null, false, "~/image/avatar.jpg", null, "Tech Events", false, "Ziad", "Organizier" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
