using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HW4.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Gender", "Password", "UpdatedDate", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", "user", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1" },
                    { 2, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", "123", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria" },
                    { 3, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", "123", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "alex" },
                    { 4, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", "123", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "olga" },
                    { 5, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", "123", new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "artem" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
