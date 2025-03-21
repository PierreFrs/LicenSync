using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[]
                {
                    "Id",
                    "Address",
                    "Birthdate",
                    "City",
                    "Country",
                    "CreationDate",
                    "PhoneNumber",
                    "PostalCode",
                    "ProfilePicturePath",
                    "UpdateDate",
                },
                values: new object[]
                {
                    new Guid("d8f71222-4d29-4ff4-925a-68a308552c71"),
                    "1 rue de la Paix",
                    new DateTime(1996, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    "Paris",
                    "France",
                    new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    "0123456789",
                    "75000",
                    null,
                    null,
                }
            );

            migrationBuilder.UpdateData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"),
                column: "UserId",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                column: "UserId",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"),
                column: "UserId",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("11232dcf-2f55-41d2-86f5-07026989e827"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("a4037469-38b4-4fb5-81ab-79049e16de19"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6"),
                column: "user_id",
                value: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id",
                    "AccessFailedCount",
                    "ConcurrencyStamp",
                    "Email",
                    "EmailConfirmed",
                    "FirstName",
                    "LastName",
                    "LockoutEnabled",
                    "LockoutEnd",
                    "NormalizedEmail",
                    "NormalizedUserName",
                    "PasswordHash",
                    "PhoneNumber",
                    "PhoneNumberConfirmed",
                    "SecurityStamp",
                    "TwoFactorEnabled",
                    "UserInfoId",
                    "UserName",
                },
                values: new object[]
                {
                    "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45",
                    0,
                    "a2494767-7420-4216-b433-47619c6818b1",
                    "p.fraisse@mail.com",
                    false,
                    "Pierre",
                    "Fraisse",
                    true,
                    null,
                    "P.FRAISSE@MAIL.COM",
                    "P.FRAISSE@MAIL.COM",
                    "AQAAAAIAAYagAAAAEHIpnPV0RitSgXYLnB/w9OAc0ER79cE0ZGHRiIIPIH3a1tnKIYpBGtOod+TMJsYcEg==",
                    null,
                    false,
                    "7F4FEWBMI62NF6VRYEGZBOYBBUJMILQK",
                    false,
                    new Guid("d8f71222-4d29-4ff4-925a-68a308552c71"),
                    "p.fraisse@mail.com",
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45"
            );

            migrationBuilder.DeleteData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: new Guid("d8f71222-4d29-4ff4-925a-68a308552c71")
            );

            migrationBuilder.UpdateData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"),
                column: "UserId",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                column: "UserId",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"),
                column: "UserId",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("11232dcf-2f55-41d2-86f5-07026989e827"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("a4037469-38b4-4fb5-81ab-79049e16de19"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );

            migrationBuilder.UpdateData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6"),
                column: "user_id",
                value: "66507cea-60ef-4587-b661-a15a9a9960c6"
            );
        }
    }
}
