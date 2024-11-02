using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "PasswordHash", "PasswordSalt", "RoleId", "Username" },
                values: new object[] { 1, new byte[] { 7, 135, 68, 233, 146, 124, 243, 116, 245, 101, 131, 29, 226, 246, 152, 83, 9, 112, 20, 26, 145, 187, 61, 129, 81, 114, 19, 46, 78, 110, 31, 173, 146, 83, 119, 237, 145, 124, 246, 33, 11, 167, 229, 83, 152, 244, 135, 138, 71, 210, 58, 26, 244, 124, 130, 7, 235, 34, 158, 143, 149, 45, 23, 95 }, new byte[] { 61, 137, 146, 221, 120, 4, 199, 189, 128, 24, 118, 112, 121, 8, 221, 7, 206, 186, 208, 71, 133, 4, 253, 192, 129, 114, 131, 66, 129, 244, 180, 154, 251, 95, 182, 4, 37, 155, 196, 102, 246, 215, 9, 51, 133, 77, 250, 37, 109, 124, 249, 121, 134, 153, 214, 198, 45, 216, 148, 246, 5, 17, 143, 44, 43, 20, 46, 71, 191, 55, 225, 113, 1, 34, 206, 181, 26, 68, 92, 146, 54, 231, 62, 232, 89, 42, 244, 28, 80, 61, 93, 146, 243, 244, 206, 219, 12, 20, 188, 37, 75, 93, 115, 199, 78, 243, 87, 241, 235, 193, 138, 172, 67, 243, 207, 120, 93, 240, 193, 49, 11, 155, 233, 203, 184, 128, 88, 113 }, 1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
