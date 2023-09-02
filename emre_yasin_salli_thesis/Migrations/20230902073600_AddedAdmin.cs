using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace emre_yasin_salli_thesis.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e318b243-006a-4c2f-a8ab-5111d46bceb2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01fcd2fa-e4c6-42e5-bbef-6f18191e9612", "c11d3586-1205-4932-a014-bc0d77ca2249" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01fcd2fa-e4c6-42e5-bbef-6f18191e9612");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c11d3586-1205-4932-a014-bc0d77ca2249");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7041fdc4-874e-4450-85ae-3852b91361d6", null, "admin", "ADMİN" },
                    { "8b4e37cb-7002-42f4-a503-ab003edc49db", null, "member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b9a7333d-e18a-4a08-805c-b91f1b6e06ad", 0, "9e2396ea-b8a5-462c-aa9d-2d9a92204cd1", "admin@email.com", false, "admin", "admin", false, null, "ADMİN@EMAİL.COM", "ADMİN@EMAİL.COM", "AQAAAAIAAYagAAAAEH392poxhCJotbMIvhXJEZdnGHYhsW/xUATLuTazAI/SMysAPcreoeHGL6F23J0Vdw==", null, false, "d2a966b2-a9b4-4851-ae79-a4b8ad3d45c0", false, "admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7041fdc4-874e-4450-85ae-3852b91361d6", "b9a7333d-e18a-4a08-805c-b91f1b6e06ad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b4e37cb-7002-42f4-a503-ab003edc49db");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7041fdc4-874e-4450-85ae-3852b91361d6", "b9a7333d-e18a-4a08-805c-b91f1b6e06ad" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7041fdc4-874e-4450-85ae-3852b91361d6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9a7333d-e18a-4a08-805c-b91f1b6e06ad");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01fcd2fa-e4c6-42e5-bbef-6f18191e9612", null, "admin", "ADMİN" },
                    { "e318b243-006a-4c2f-a8ab-5111d46bceb2", null, "member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c11d3586-1205-4932-a014-bc0d77ca2249", 0, "c18f4429-2f9b-4c1b-8b96-dc145b85b778", "admin@email.com", false, "admin", "admin", false, null, "ADMİN@EMAİL.COM", "ADMİN@EMAİL.COM", null, null, false, "27aeb457-137c-401d-b1c6-3e72fdf59592", false, "admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "01fcd2fa-e4c6-42e5-bbef-6f18191e9612", "c11d3586-1205-4932-a014-bc0d77ca2249" });
        }
    }
}
