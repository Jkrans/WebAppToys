using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBidsValidationRequirement3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Bids",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c8bcd4-2819-4584-bdfb-9d1b03056026",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67cb3529-8c05-4139-b5b0-7047398a419f", "AQAAAAIAAYagAAAAEE+LBBAC/ZiFRRAC623kaepvSDzml8fWmnBOZb86dlud7xPa5do6XJOuprqHCriq0A==", "b429ca1b-5033-479a-ba9b-f5940348ff68" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Trade_Id",
                table: "Bids",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Bids",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c8bcd4-2819-4584-bdfb-9d1b03056026",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2a57b0a-b6c6-4375-90ea-12019097dd91", "AQAAAAIAAYagAAAAEGgT1J4tQFLK+Y/6X/dVnMSO3HtakofvrE3TKvSGGomokWxol/C+BWKWPVe+rIuQkQ==", "e8682013-6df5-4f81-b1b1-6426e123f057" });
        }
    }
}
