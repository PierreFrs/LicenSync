using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "album",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    album_title = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    album_visual_path = table.Column<string>(
                        type: "nvarchar(255)",
                        maxLength: 255,
                        nullable: true
                    ),
                    creation_date = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        defaultValueSql: "GETDATE()"
                    ),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_album", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedName = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "contribution",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    label = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    creation_date = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        defaultValueSql: "GETDATE()"
                    ),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contribution", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    label = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    creation_date = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        defaultValueSql: "GETDATE()"
                    ),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.ID);
                }
            );

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePicturePath = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true
                    ),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "track",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    track_title = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    length = table.Column<string>(
                        type: "nvarchar(8)",
                        maxLength: 8,
                        nullable: false
                    ),
                    audio_file_path = table.Column<string>(
                        type: "nvarchar(255)",
                        maxLength: 255,
                        nullable: false
                    ),
                    user_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    record_label = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: true
                    ),
                    track_visual_path = table.Column<string>(
                        type: "nvarchar(255)",
                        maxLength: 255,
                        nullable: true
                    ),
                    first_genre_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    secondary_genre_id = table.Column<Guid>(
                        type: "uniqueidentifier",
                        nullable: true
                    ),
                    album_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    blockchain_hash_id = table.Column<Guid>(
                        type: "uniqueidentifier",
                        nullable: true
                    ),
                    creation_date = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        defaultValueSql: "GETDATE()"
                    ),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_track", x => x.ID);
                    table.ForeignKey(
                        name: "FK_track_album_album_id",
                        column: x => x.album_id,
                        principalTable: "album",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_track_genre_first_genre_id",
                        column: x => x.first_genre_id,
                        principalTable: "genre",
                        principalColumn: "ID"
                    );
                    table.ForeignKey(
                        name: "FK_track_genre_secondary_genre_id",
                        column: x => x.secondary_genre_id,
                        principalTable: "genre",
                        principalColumn: "ID"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedUserName = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    Email = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    NormalizedEmail = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(
                        type: "datetimeoffset",
                        nullable: true
                    ),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "artist",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstname = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    lastname = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    track_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    contribution_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creation_date = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false,
                        defaultValueSql: "GETDATE()"
                    ),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artist", x => x.ID);
                    table.ForeignKey(
                        name: "FK_artist_contribution_contribution_id",
                        column: x => x.contribution_id,
                        principalTable: "contribution",
                        principalColumn: "ID"
                    );
                    table.ForeignKey(
                        name: "FK_artist_track_track_id",
                        column: x => x.track_id,
                        principalTable: "track",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true
                    ),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_AspNetUserLogins",
                        x => new { x.LoginProvider, x.ProviderKey }
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_AspNetUserTokens",
                        x => new
                        {
                            x.UserId,
                            x.LoginProvider,
                            x.Name,
                        }
                    );
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.InsertData(
                table: "album",
                columns: new[]
                {
                    "ID",
                    "album_title",
                    "album_visual_path",
                    "update_date",
                    "UserId",
                },
                values: new object[,]
                {
                    {
                        new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"),
                        "The Dark Side of the Moon",
                        "/src/backend/Uploads/Pictures/AlbumVisuals/07373bbe-1a4a-4e43-a177-5260e80b497athe_dark_side_of_the_moon.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                        "disraeli_gears",
                        "/src/backend/Uploads/Pictures/AlbumVisuals/42ddc682-eaa6-4ae4-bca6-c9672e1dfa14disraeli_gears.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"),
                        "The Wall",
                        "/src/backend/Uploads/Pictures/AlbumVisuals/a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4athe_wall.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                }
            );

            migrationBuilder.InsertData(
                table: "contribution",
                columns: new[] { "ID", "label", "update_date" },
                values: new object[,]
                {
                    { new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"), "Musique", null },
                    {
                        new Guid("a5222888-0597-4072-8852-037bf1a0cf91"),
                        "Musique et paroles",
                        null,
                    },
                    { new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"), "Paroles", null },
                }
            );

            migrationBuilder.InsertData(
                table: "genre",
                columns: new[] { "ID", "label", "update_date" },
                values: new object[,]
                {
                    { new Guid("b1538091-1957-401b-932b-cef92e05654f"), "Rap", null },
                    { new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"), "Rock", null },
                    { new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"), "Pop", null },
                }
            );

            migrationBuilder.InsertData(
                table: "track",
                columns: new[]
                {
                    "ID",
                    "album_id",
                    "audio_file_path",
                    "blockchain_hash_id",
                    "first_genre_id",
                    "length",
                    "record_label",
                    "secondary_genre_id",
                    "track_title",
                    "track_visual_path",
                    "update_date",
                    "user_id",
                },
                values: new object[,]
                {
                    {
                        new Guid("11232dcf-2f55-41d2-86f5-07026989e827"),
                        new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"),
                        "/src/backend/Uploads/AudioFiles/11232dcf-2f55-41d2-86f5-07026989e827_on_the_run.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "03:45",
                        null,
                        new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"),
                        "On the Run",
                        "/src/backend/Uploads/Pictures/TrackVisuals/11232dcf-2f55-41d2-86f5-07026989e827_on_the_run.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971"),
                        new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"),
                        "/src/backend/Uploads/AudioFiles/12fba9b9-43de-41fa-9086-5ce2a34cb971_wish_you_were_here.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "05:40",
                        null,
                        new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"),
                        "Wish You Were Here",
                        "/src/backend/Uploads/Pictures/TrackVisuals/12fba9b9-43de-41fa-9086-5ce2a34cb971_wish_you_were_here.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"),
                        new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                        "/src/backend/Uploads/AudioFiles/1c4611b8-0483-48af-8a47-eb57215cc6f1_white_room.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "04:58",
                        null,
                        null,
                        "White Room",
                        "/src/backend/Uploads/Pictures/TrackVisuals/1c4611b8-0483-48af-8a47-eb57215cc6f1_white_room.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d"),
                        new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"),
                        "/src/backend/Uploads/AudioFiles/385dcdba-b43b-4f46-8d04-51da0b863b1d_hey_you.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "04:40",
                        null,
                        null,
                        "Hey You",
                        "/src/backend/Uploads/Pictures/TrackVisuals/385dcdba-b43b-4f46-8d04-51da0b863b1d_hey_you.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"),
                        new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"),
                        "/src/backend/Uploads/AudioFiles/8b74701f-2636-42a0-af8e-ff353169a6c2_speak_to_me.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "01:30",
                        null,
                        new Guid("d7a30e56-6073-46e6-86d1-4a4dc7870d75"),
                        "Speak to Me",
                        "/src/backend/Uploads/Pictures/TrackVisuals/8b74701f-2636-42a0-af8e-ff353169a6c2_speak_to_me.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("a4037469-38b4-4fb5-81ab-79049e16de19"),
                        new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"),
                        "/src/backend/Uploads/AudioFiles/a4037469-38b4-4fb5-81ab-79049e16de19_comfortably_numb.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "06:21",
                        null,
                        null,
                        "Comfortably Numb",
                        "/src/backend/Uploads/Pictures/TrackVisuals/a4037469-38b4-4fb5-81ab-79049e16de19_comfortably_numb.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9"),
                        new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"),
                        "/src/backend/Uploads/AudioFiles/d23d9b8e-50ff-4322-b454-00539e009aa9_time.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "07:05",
                        null,
                        null,
                        "Time",
                        "/src/backend/Uploads/Pictures/TrackVisuals/d23d9b8e-50ff-4322-b454-00539e009aa9_time.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7"),
                        new Guid("07373bbe-1a4a-4e43-a177-5260e80b497a"),
                        "/src/backend/Uploads/AudioFiles/d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7_breathe.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "02:49",
                        null,
                        null,
                        "Breathe",
                        "/src/backend/Uploads/Pictures/TrackVisuals/d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7_breathe.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a"),
                        new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                        "/src/backend/Uploads/AudioFiles/e4c4d81a-cbd5-4087-acc5-f23a4cb6315a_tales_of_brave_ulysses.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "02:46",
                        null,
                        new Guid("b1538091-1957-401b-932b-cef92e05654f"),
                        "Tales of Brave Ulysses",
                        "/src/backend/Uploads/Pictures/TrackVisuals/e4c4d81a-cbd5-4087-acc5-f23a4cb6315a_tales_of_brave_ulysses.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f"),
                        new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                        "/src/backend/Uploads/AudioFiles/feb3ccab-ddfe-4e32-92ae-9f34d107c82f_sunshine_of_your_love.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "04:10",
                        null,
                        new Guid("b1538091-1957-401b-932b-cef92e05654f"),
                        "Sunshine of Your Love",
                        "/src/backend/Uploads/Pictures/TrackVisuals/feb3ccab-ddfe-4e32-92ae-9f34d107c82f_sunshine_of_your_love.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                    {
                        new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6"),
                        new Guid("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a"),
                        "/src/backend/Uploads/AudioFiles/ff3f70d3-3dbf-4e72-b44f-94147378bbe6_another_brick_in_the_wall_pt_2.mp3",
                        null,
                        new Guid("d3ed7bf3-4f90-4b5d-8006-76f4560402ba"),
                        "03:59",
                        null,
                        null,
                        "Another Brick in the Wall, Pt. 2",
                        "/src/backend/Uploads/Pictures/TrackVisuals/ff3f70d3-3dbf-4e72-b44f-94147378bbe6_another_brick_in_the_wall_pt_2.jpg",
                        null,
                        "66507cea-60ef-4587-b661-a15a9a9960c6",
                    },
                }
            );

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
                        new Guid("0a4eebf1-9578-4c8b-95d4-98a3857543c4"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "Syd",
                        "Barrett",
                        new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971"),
                        null,
                    },
                    {
                        new Guid("30800195-feb4-472d-8b9d-842c8e8330ee"),
                        new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"),
                        "Jack",
                        "Bruce",
                        new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"),
                        null,
                    },
                    {
                        new Guid("332b8af0-e2af-41ac-a2ad-4bafdf26492e"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "Nick",
                        "Mason",
                        new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"),
                        null,
                    },
                    {
                        new Guid("3be31500-5217-47f5-91fc-3a2a1a166220"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "Roger",
                        "Waters",
                        new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6"),
                        null,
                    },
                    {
                        new Guid("3e235143-6286-4c61-9190-dcdb4f3aab23"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "Richard",
                        "Wright",
                        new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9"),
                        null,
                    },
                    {
                        new Guid("9cefa4a8-ac72-4551-bb2c-14b4c2aaa4ed"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "Roger",
                        "Waters",
                        new Guid("11232dcf-2f55-41d2-86f5-07026989e827"),
                        null,
                    },
                    {
                        new Guid("a0da1f96-4cdf-4490-8f70-be07741229a3"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "David",
                        "Gilmour",
                        new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"),
                        null,
                    },
                    {
                        new Guid("ac862365-242f-4932-b9a3-71a354386f20"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "David",
                        "Gilmour",
                        new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d"),
                        null,
                    },
                    {
                        new Guid("acc6c5a9-cdd9-4841-8c02-dc56e4e6d6b6"),
                        new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"),
                        "Eric",
                        "Clapton",
                        new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f"),
                        null,
                    },
                    {
                        new Guid("db56ed2a-ff62-4083-a57c-70b0b050aeb8"),
                        new Guid("ca64c63d-f5eb-47d6-aeee-b720c80ad82a"),
                        "Eric",
                        "Clapton",
                        new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"),
                        null,
                    },
                    {
                        new Guid("f7c9d153-a0e2-4647-841d-bf525fbe0ab5"),
                        new Guid("073fb88b-95d8-4b4e-85e3-ad9637a681a4"),
                        "Roger",
                        "Waters",
                        new Guid("a4037469-38b4-4fb5-81ab-79049e16de19"),
                        null,
                    },
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_artist_contribution_id",
                table: "artist",
                column: "contribution_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_artist_track_id",
                table: "artist",
                column: "track_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserInfoId",
                table: "AspNetUsers",
                column: "UserInfoId"
            );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "IX_track_album_id",
                table: "track",
                column: "album_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_track_first_genre_id",
                table: "track",
                column: "first_genre_id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_track_secondary_genre_id",
                table: "track",
                column: "secondary_genre_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "artist");

            migrationBuilder.DropTable(name: "AspNetRoleClaims");

            migrationBuilder.DropTable(name: "AspNetUserClaims");

            migrationBuilder.DropTable(name: "AspNetUserLogins");

            migrationBuilder.DropTable(name: "AspNetUserRoles");

            migrationBuilder.DropTable(name: "AspNetUserTokens");

            migrationBuilder.DropTable(name: "contribution");

            migrationBuilder.DropTable(name: "track");

            migrationBuilder.DropTable(name: "AspNetRoles");

            migrationBuilder.DropTable(name: "AspNetUsers");

            migrationBuilder.DropTable(name: "album");

            migrationBuilder.DropTable(name: "genre");

            migrationBuilder.DropTable(name: "UserInfos");
        }
    }
}
