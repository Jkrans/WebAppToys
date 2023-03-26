using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ListingExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Listing",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c8bcd4-2819-4584-bdfb-9d1b03056026",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "345b69be-d639-4e71-8bc1-9c4ed627f7e1", "AQAAAAIAAYagAAAAEGULi3qGjcf6n23LAM06QBgnJQlsP7iEfCfpMPdS1r/l+Jr2D6jglGN0ZtVFXYFV7g==", "93d1a9dd-bd6c-4f9c-a59e-a822f3c6a94c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Listing");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b9c8bcd4-2819-4584-bdfb-9d1b03056026",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd5e6b26-31ac-4f13-a1c0-4c08c9029dbd", "AQAAAAIAAYagAAAAEDN33bliUdiwqRvUBcrpF/LHG1+SzpJ3sNUeJuKaJSbbsdlAUYYJBR4bvTlfxG7KNw==", "23fd60d6-b740-4c87-ab5c-c5382a552b94" });
        }
    }
}
