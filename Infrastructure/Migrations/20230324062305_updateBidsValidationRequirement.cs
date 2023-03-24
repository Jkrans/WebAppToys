using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBidsValidationRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c8bcd4-2819-4584-bdfb-9d1b03056026",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed72b0e8-551c-49f1-af95-d629c22fd664", "AQAAAAIAAYagAAAAEFjmtMzGOnT+5/2ULG93vq6sGd8GXi46B9h3crbCU41YszrC015mjlRnU1Ferw2MpA==", "97ba5ada-5279-4932-a21d-a99f9fa0af63" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c8bcd4-2819-4584-bdfb-9d1b03056026",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7181685b-b600-4614-a83e-28949cf5feb1", "AQAAAAIAAYagAAAAEHbuLoAgSeu0slmYD7G9fKJWFJT4njD+cKmGs6W1Y0+AmkNk/DjPgY0nqf9tbylevQ==", "ab80c941-e566-444b-8627-8c0a76d9fd99" });
        }
    }
}
