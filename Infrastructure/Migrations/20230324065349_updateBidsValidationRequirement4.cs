using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBidsValidationRequirement4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Trade_Id",
                table: "Bids",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c8bcd4-2819-4584-bdfb-9d1b03056026",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd5e6b26-31ac-4f13-a1c0-4c08c9029dbd", "AQAAAAIAAYagAAAAEDN33bliUdiwqRvUBcrpF/LHG1+SzpJ3sNUeJuKaJSbbsdlAUYYJBR4bvTlfxG7KNw==", "23fd60d6-b740-4c87-ab5c-c5382a552b94" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Trade_Id",
                table: "Bids",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c8bcd4-2819-4584-bdfb-9d1b03056026",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67cb3529-8c05-4139-b5b0-7047398a419f", "AQAAAAIAAYagAAAAEE+LBBAC/ZiFRRAC623kaepvSDzml8fWmnBOZb86dlud7xPa5do6XJOuprqHCriq0A==", "b429ca1b-5033-479a-ba9b-f5940348ff68" });
        }
    }
}
