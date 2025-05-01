using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45");

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("0a4eebf1-9578-4c8b-95d4-98a3857543c4"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("30800195-feb4-472d-8b9d-842c8e8330ee"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("332b8af0-e2af-41ac-a2ad-4bafdf26492e"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("37446071-3306-4a28-90be-beb947241c12"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("3be31500-5217-47f5-91fc-3a2a1a166220"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("3e235143-6286-4c61-9190-dcdb4f3aab23"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("9cefa4a8-ac72-4551-bb2c-14b4c2aaa4ed"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("a0da1f96-4cdf-4490-8f70-be07741229a3"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("ac862365-242f-4932-b9a3-71a354386f20"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("acc6c5a9-cdd9-4841-8c02-dc56e4e6d6b6"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("b35ff007-544c-4361-accf-aea0328441a8"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("db56ed2a-ff62-4083-a57c-70b0b050aeb8"));

            migrationBuilder.DeleteData(
                table: "artist",
                keyColumn: "ID",
                keyValue: new Guid("f7c9d153-a0e2-4647-841d-bf525fbe0ab5"));

            migrationBuilder.DeleteData(
                table: "contribution",
                keyColumn: "ID",
                keyValue: new Guid("a5222888-0597-4072-8852-037bf1a0cf91"));

            migrationBuilder.DeleteData(
                table: "UserInfos",
                keyColumn: "Id",
                keyValue: new Guid("d8f71222-4d29-4ff4-925a-68a308552c71"));

            migrationBuilder.DeleteData(
                table: "contribution",
                keyColumn: "ID",
                keyValue: new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"));

            migrationBuilder.DeleteData(
                table: "contribution",
                keyColumn: "ID",
                keyValue: new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("11232dcf-2f55-41d2-86f5-07026989e827"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("a4037469-38b4-4fb5-81ab-79049e16de19"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f"));

            migrationBuilder.DeleteData(
                table: "track",
                keyColumn: "ID",
                keyValue: new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6"));

            migrationBuilder.DeleteData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"));

            migrationBuilder.DeleteData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"));

            migrationBuilder.DeleteData(
                table: "album",
                keyColumn: "ID",
                keyValue: new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"));

            migrationBuilder.DeleteData(
                table: "genre",
                keyColumn: "ID",
                keyValue: new Guid("b1538091-1957-401b-932b-cef92e05654f"));

            migrationBuilder.DeleteData(
                table: "genre",
                keyColumn: "ID",
                keyValue: new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"));

            migrationBuilder.DeleteData(
                table: "genre",
                keyColumn: "ID",
                keyValue: new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "track",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "album",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "track");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "album");

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "Id", "Address", "Birthdate", "City", "Country", "CreationDate", "PhoneNumber", "PostalCode", "ProfilePicturePath", "UpdateDate" },
                values: new object[] { new Guid("d8f71222-4d29-4ff4-925a-68a308552c71"), "1 rue de la Paix", new DateTime(1996, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paris", "France", new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456789", "75000", null, null });

            migrationBuilder.InsertData(
                table: "album",
                columns: new[] { "ID", "album_title", "album_visual_path", "update_date", "UserId" },
                values: new object[,]
                {
                    { new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"), "The Dark Side of the Moon", "/src/backend/Uploads/Pictures/AlbumVisuals/07373bbe-1a4a-4e43-a177-5260e80b497athe_dark_side_of_the_moon.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"), "disraeli_gears", "/src/backend/Uploads/Pictures/AlbumVisuals/42ddc682-eaa6-4ae4-bca6-c9672e1dfa14disraeli_gears.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"), "The Wall", "/src/backend/Uploads/Pictures/AlbumVisuals/a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4athe_wall.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" }
                });

            migrationBuilder.InsertData(
                table: "contribution",
                columns: new[] { "ID", "label", "update_date" },
                values: new object[,]
                {
                    { new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Musique", null },
                    { new Guid("a5222888-0597-4072-8852-037bf1a0cf91"), "Musique et paroles", null },
                    { new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"), "Paroles", null }
                });

            migrationBuilder.InsertData(
                table: "genre",
                columns: new[] { "ID", "label", "update_date" },
                values: new object[,]
                {
                    { new Guid("b1538091-1957-401b-932b-cef92e05654f"), "Rap", null },
                    { new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "Rock", null },
                    { new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"), "Pop", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserInfoId", "UserName" },
                values: new object[] { "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45", 0, "a2494767-7420-4216-b433-47619c6818b1", "p.fraisse@mail.com", false, "Pierre", "Fraisse", true, null, "P.FRAISSE@MAIL.COM", "P.FRAISSE@MAIL.COM", "AQAAAAIAAYagAAAAEHIpnPV0RitSgXYLnB/w9OAc0ER79cE0ZGHRiIIPIH3a1tnKIYpBGtOod+TMJsYcEg==", null, false, "7F4FEWBMI62NF6VRYEGZBOYBBUJMILQK", false, new Guid("d8f71222-4d29-4ff4-925a-68a308552c71"), "p.fraisse@mail.com" });

            migrationBuilder.InsertData(
                table: "track",
                columns: new[] { "ID", "album_id", "audio_file_path", "blockchain_hash_id", "first_genre_id", "length", "record_label", "secondary_genre_id", "track_title", "track_visual_path", "update_date", "user_id" },
                values: new object[,]
                {
                    { new Guid("11232dcf-2f55-41d2-86f5-07026989e827"), new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"), "/src/backend/Uploads/AudioFiles/11232dcf-2f55-41d2-86f5-07026989e827_on_the_run.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "03:45", null, new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"), "On the Run", "/src/backend/Uploads/Pictures/TrackVisuals/11232dcf-2f55-41d2-86f5-07026989e827_on_the_run.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971"), new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"), "/src/backend/Uploads/AudioFiles/12fba9b9-43de-41fa-9086-5ce2a34cb971_wish_you_were_here.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "05:40", null, new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"), "Wish You Were Here", "/src/backend/Uploads/Pictures/TrackVisuals/12fba9b9-43de-41fa-9086-5ce2a34cb971_wish_you_were_here.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"), new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"), "/src/backend/Uploads/AudioFiles/1c4611b8-0483-48af-8a47-eb57215cc6f1_white_room.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "04:58", null, null, "White Room", "/src/backend/Uploads/Pictures/TrackVisuals/1c4611b8-0483-48af-8a47-eb57215cc6f1_white_room.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d"), new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"), "/src/backend/Uploads/AudioFiles/385dcdba-b43b-4f46-8d04-51da0b863b1d_hey_you.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "04:40", null, null, "Hey You", "/src/backend/Uploads/Pictures/TrackVisuals/385dcdba-b43b-4f46-8d04-51da0b863b1d_hey_you.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"), new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"), "/src/backend/Uploads/AudioFiles/8b74701f-2636-42a0-af8e-ff353169a6c2_speak_to_me.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "01:30", null, new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"), "Speak to Me", "/src/backend/Uploads/Pictures/TrackVisuals/8b74701f-2636-42a0-af8e-ff353169a6c2_speak_to_me.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("a4037469-38b4-4fb5-81ab-79049e16de19"), new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"), "/src/backend/Uploads/AudioFiles/a4037469-38b4-4fb5-81ab-79049e16de19_comfortably_numb.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "06:21", null, null, "Comfortably Numb", "/src/backend/Uploads/Pictures/TrackVisuals/a4037469-38b4-4fb5-81ab-79049e16de19_comfortably_numb.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9"), new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"), "/src/backend/Uploads/AudioFiles/d23d9b8e-50ff-4322-b454-00539e009aa9_time.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "07:05", null, null, "Time", "/src/backend/Uploads/Pictures/TrackVisuals/d23d9b8e-50ff-4322-b454-00539e009aa9_time.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7"), new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"), "/src/backend/Uploads/AudioFiles/d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7_breathe.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "02:49", null, null, "Breathe", "/src/backend/Uploads/Pictures/TrackVisuals/d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7_breathe.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a"), new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"), "/src/backend/Uploads/AudioFiles/e4c4d81a-cbd5-4087-acc5-f23a4cb6315a_tales_of_brave_ulysses.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "02:46", null, new Guid("b1538091-1957-401b-932b-cef92e05654f"), "Tales of Brave Ulysses", "/src/backend/Uploads/Pictures/TrackVisuals/e4c4d81a-cbd5-4087-acc5-f23a4cb6315a_tales_of_brave_ulysses.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f"), new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"), "/src/backend/Uploads/AudioFiles/feb3ccab-ddfe-4e32-92ae-9f34d107c82f_sunshine_of_your_love.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "04:10", null, new Guid("b1538091-1957-401b-932b-cef92e05654f"), "Sunshine of Your Love", "/src/backend/Uploads/Pictures/TrackVisuals/feb3ccab-ddfe-4e32-92ae-9f34d107c82f_sunshine_of_your_love.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" },
                    { new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6"), new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"), "/src/backend/Uploads/AudioFiles/ff3f70d3-3dbf-4e72-b44f-94147378bbe6_another_brick_in_the_wall_pt_2.mp3", null, new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "03:59", null, null, "Another Brick in the Wall, Pt. 2", "/src/backend/Uploads/Pictures/TrackVisuals/ff3f70d3-3dbf-4e72-b44f-94147378bbe6_another_brick_in_the_wall_pt_2.jpg", null, "b0d28ce7-072a-4e2e-b3a7-888b7a88fb45" }
                });

            migrationBuilder.InsertData(
                table: "artist",
                columns: new[] { "ID", "contribution_id", "firstname", "lastname", "track_id", "update_date" },
                values: new object[,]
                {
                    { new Guid("0a4eebf1-9578-4c8b-95d4-98a3857543c4"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Syd", "Barrett", new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971"), null },
                    { new Guid("30800195-feb4-472d-8b9d-842c8e8330ee"), new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"), "Jack", "Bruce", new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"), null },
                    { new Guid("332b8af0-e2af-41ac-a2ad-4bafdf26492e"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Nick", "Mason", new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"), null },
                    { new Guid("37446071-3306-4a28-90be-beb947241c12"), new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"), "Roger", "Waters", new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7"), null },
                    { new Guid("3be31500-5217-47f5-91fc-3a2a1a166220"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Roger", "Waters", new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6"), null },
                    { new Guid("3e235143-6286-4c61-9190-dcdb4f3aab23"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Richard", "Wright", new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9"), null },
                    { new Guid("9cefa4a8-ac72-4551-bb2c-14b4c2aaa4ed"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Roger", "Waters", new Guid("11232dcf-2f55-41d2-86f5-07026989e827"), null },
                    { new Guid("a0da1f96-4cdf-4490-8f70-be07741229a3"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "David", "Gilmour", new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"), null },
                    { new Guid("ac862365-242f-4932-b9a3-71a354386f20"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "David", "Gilmour", new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d"), null },
                    { new Guid("acc6c5a9-cdd9-4841-8c02-dc56e4e6d6b6"), new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"), "Eric", "Clapton", new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f"), null },
                    { new Guid("b35ff007-544c-4361-accf-aea0328441a8"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Eric", "Clapton", new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a"), null },
                    { new Guid("db56ed2a-ff62-4083-a57c-70b0b050aeb8"), new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"), "Eric", "Clapton", new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"), null },
                    { new Guid("f7c9d153-a0e2-4647-841d-bf525fbe0ab5"), new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Roger", "Waters", new Guid("a4037469-38b4-4fb5-81ab-79049e16de19"), null }
                });
        }
    }
}
