using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ArtistsSeedUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "artist",
                columns: new[]
                {
                    "ID",
                    "contribution_id",
                    "firstname",
                    "lastname",
                    "track_id",
                    "update_date",
                },
                values: new object[,]
                {
                    {
                        new Guid("37446071-3306-4a28-90be-beb947241c12"),
                        new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"),
                        "Roger",
                        "Waters",
                        new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7"),
                        null,
                    },
                    {
                        new Guid("b35ff007-544c-4361-accf-aea0328441a8"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "Eric",
                        "Clapton",
                        new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a"),
                        null,
                    },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("37446071-3306-4a28-90be-beb947241c12")
            );

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("b35ff007-544c-4361-accf-aea0328441a8")
            );
        }
    }
}
