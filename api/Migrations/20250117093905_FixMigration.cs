using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class FixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c2ca696-db97-4cfb-af1b-c4dd1b6a4e69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8ce293e-81da-411e-8895-c15048b974e4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "436f688c-a946-432f-8728-601dbb09de46", null, "Admin", "ADMIN" },
                    { "7c82ff79-d94e-4c91-8d2a-40cc5a36e7ae", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "436f688c-a946-432f-8728-601dbb09de46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c82ff79-d94e-4c91-8d2a-40cc5a36e7ae");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c2ca696-db97-4cfb-af1b-c4dd1b6a4e69", null, "User", "USER" },
                    { "d8ce293e-81da-411e-8895-c15048b974e4", null, "Admin", "ADMIN" }
                });
        }
    }
}
