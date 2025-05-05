// AppContextSeed.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 18/09/2024
// Fichier Modifié le : 18/09/2024
// Code développé pour le projet : Infrastructure

using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class AppContextSeed
{
    static readonly Guid _darkSideOfTheMoonAlbumId = new("07373bbe-1a4a-4e43-a177-5260e80b497a");
    static readonly Guid _disraeliGearsAlbumId = new("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14");
    static readonly Guid _theWallAlbumId = new("a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4a");
    static readonly string _userId = new("b0d28ce7-072a-4e2e-b3a7-888b7a88fb45");
    static readonly Guid _musiqueContributionId = new("073fb88b-95d8-4b4e-85e3-ad9637a681a4");
    static readonly Guid _parolesContributionId = new("ca64c63d-f5eb-47d6-aeee-b720c80ad82a");
    static readonly Guid _musiqueEtParolesContributionId = new(
        "a5222888-0597-4072-8852-037bf1a0cf91"
    );
    static readonly Guid _rockGenreId = new("d3ed7bf3-4f90-4b5d-8006-76f4560402ba");
    static readonly Guid _popGenreId = new("d7a30e56-6073-46e6-86d1-4a4dc7870d75");
    static readonly Guid _rapGenreId = new("b1538091-1957-401b-932b-cef92e05654f");

    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Genres.AnyAsync())
            await SeedGenres(dbContext);

        if (!await dbContext.Contributions.AnyAsync())
            await SeedContributions(dbContext);

        if (!await dbContext.Albums.AnyAsync())
            await SeedAlbums(dbContext);

        if (!await dbContext.Tracks.AnyAsync())
            await SeedTracks(dbContext);

        if (!await dbContext.Artists.AnyAsync())
            await SeedArtists(dbContext);

        if (!await dbContext.TrackArtistContributions.AnyAsync())
            await SeedTrackArtistContributions(dbContext);

        await SeedRoles(dbContext);

        if (!await dbContext.Users.AnyAsync())
            await SeedAppUsers(dbContext);

        if (!await dbContext.UserRoles.AnyAsync())
            await SeedUserRoles(dbContext);

        if (!await dbContext.UserInfos.AnyAsync())
            await SeedUserInfos(dbContext);
    }

    private static async Task SeedGenres(ApplicationDbContext dbContext)
    {
        await dbContext.Genres.AddRangeAsync(
            new Genre { Id = _rockGenreId, Label = "Rock" },
            new Genre { Id = _popGenreId, Label = "Pop" },
            new Genre { Id = _rapGenreId, Label = "Rap" }
        );

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedContributions(ApplicationDbContext dbContext)
    {
        await dbContext.Contributions.AddRangeAsync(
            new Contribution { Id = _musiqueContributionId, Label = "Musique" },
            new Contribution { Id = _parolesContributionId, Label = "Paroles" },
            new Contribution { Id = _musiqueEtParolesContributionId, Label = "Musique et paroles" }
        );

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedAlbums(ApplicationDbContext dbContext)
    {
        await dbContext.Albums.AddRangeAsync(
            new Album
            {
                Id = _darkSideOfTheMoonAlbumId,
                AlbumTitle = "The Dark Side of the Moon",
                UserId = _userId,
                RecordLabel = "Harvest Records",
                FirstGenreId = _rockGenreId,
                SecondaryGenreId = _popGenreId,
                AlbumVisualPath =
                    "/src/backend/Uploads/Pictures/AlbumVisuals/07373bbe-1a4a-4e43-a177-5260e80b497athe_dark_side_of_the_moon.jpg",
                ReleaseDate = DateTime.Now,
            },
            new Album
            {
                Id = _disraeliGearsAlbumId,
                AlbumTitle = "disraeli_gears",
                UserId = _userId,
                RecordLabel = "Reaction",
                FirstGenreId = _rockGenreId,
                SecondaryGenreId = _popGenreId,
                AlbumVisualPath =
                    "/src/backend/Uploads/Pictures/AlbumVisuals/42ddc682-eaa6-4ae4-bca6-c9672e1dfa14disraeli_gears.jpg",
                ReleaseDate = DateTime.Now,
            },
            new Album
            {
                Id = _theWallAlbumId,
                AlbumTitle = "The Wall",
                RecordLabel = "Harvest Records",
                FirstGenreId = _rockGenreId,
                SecondaryGenreId = _popGenreId,
                UserId = _userId,
                AlbumVisualPath =
                    "/src/backend/Uploads/Pictures/AlbumVisuals/a0b0b6e2-0b7e-4b4e-8f3d-3e9b8e1b1c4athe_wall.jpg",
                ReleaseDate = DateTime.Now,
            }
        );

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedTracks(ApplicationDbContext dbContext)
    {
        await dbContext.Tracks.AddRangeAsync(
            new Track
            {
                Id = new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2"),
                TrackTitle = "Speak to Me",
                Length = "01:30",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/8b74701f-2636-42a0-af8e-ff353169a6c2_speak_to_me.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/8b74701f-2636-42a0-af8e-ff353169a6c2_speak_to_me.jpg",
                UserId = _userId,
                AlbumId = _darkSideOfTheMoonAlbumId, // The Dark Side of the Moon
                FirstGenreId = _rockGenreId,
                SecondaryGenreId = _popGenreId,
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7"),
                TrackTitle = "Breathe",
                Length = "02:49",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7_breathe.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7_breathe.jpg",
                UserId = _userId,
                AlbumId = _darkSideOfTheMoonAlbumId, // The Dark Side of the Moon
                FirstGenreId = _rockGenreId,
                SecondaryGenreId = null,
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("11232dcf-2f55-41d2-86f5-07026989e827"),
                TrackTitle = "On the Run",
                Length = "03:45",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/11232dcf-2f55-41d2-86f5-07026989e827_on_the_run.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/11232dcf-2f55-41d2-86f5-07026989e827_on_the_run.jpg",
                UserId = _userId,
                AlbumId = _darkSideOfTheMoonAlbumId, // The Dark Side of the Moon
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = _popGenreId, // Pop
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9"),
                TrackTitle = "Time",
                Length = "07:05",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/d23d9b8e-50ff-4322-b454-00539e009aa9_time.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/d23d9b8e-50ff-4322-b454-00539e009aa9_time.jpg",
                UserId = _userId,
                AlbumId = _darkSideOfTheMoonAlbumId, // The Dark Side of the Moon
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = null,
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971"),
                TrackTitle = "Wish You Were Here",
                Length = "05:40",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/12fba9b9-43de-41fa-9086-5ce2a34cb971_wish_you_were_here.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/12fba9b9-43de-41fa-9086-5ce2a34cb971_wish_you_were_here.jpg",
                UserId = _userId,
                AlbumId = _theWallAlbumId, // The Wall
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = _popGenreId, // Pop
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("a4037469-38b4-4fb5-81ab-79049e16de19"),
                TrackTitle = "Comfortably Numb",
                Length = "06:21",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/a4037469-38b4-4fb5-81ab-79049e16de19_comfortably_numb.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/a4037469-38b4-4fb5-81ab-79049e16de19_comfortably_numb.jpg",
                UserId = _userId,
                AlbumId = _theWallAlbumId, // The Wall
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = null,
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6"),
                TrackTitle = "Another Brick in the Wall, Pt. 2",
                Length = "03:59",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/ff3f70d3-3dbf-4e72-b44f-94147378bbe6_another_brick_in_the_wall_pt_2.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/ff3f70d3-3dbf-4e72-b44f-94147378bbe6_another_brick_in_the_wall_pt_2.jpg",
                UserId = _userId,
                AlbumId = _theWallAlbumId, // The Wall
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = null,
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d"),
                TrackTitle = "Hey You",
                Length = "04:40",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/385dcdba-b43b-4f46-8d04-51da0b863b1d_hey_you.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/385dcdba-b43b-4f46-8d04-51da0b863b1d_hey_you.jpg",
                UserId = _userId,
                AlbumId = _theWallAlbumId, // The Wall
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = null,
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f"),
                TrackTitle = "Sunshine of Your Love",
                Length = "04:10",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/feb3ccab-ddfe-4e32-92ae-9f34d107c82f_sunshine_of_your_love.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/feb3ccab-ddfe-4e32-92ae-9f34d107c82f_sunshine_of_your_love.jpg",
                UserId = _userId,
                AlbumId = _disraeliGearsAlbumId, // Disraeli Gears
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = new Guid("b1538091-1957-401b-932b-cef92e05654f"), // Psychedelic Rock
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1"),
                TrackTitle = "White Room",
                Length = "04:58",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/1c4611b8-0483-48af-8a47-eb57215cc6f1_white_room.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/1c4611b8-0483-48af-8a47-eb57215cc6f1_white_room.jpg",
                UserId = _userId,
                AlbumId = _disraeliGearsAlbumId, // Disraeli Gears
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = null,
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            },
            new Track
            {
                Id = new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a"),
                TrackTitle = "Tales of Brave Ulysses",
                Length = "02:46",
                AudioFilePath =
                    "/src/backend/Uploads/AudioFiles/e4c4d81a-cbd5-4087-acc5-f23a4cb6315a_tales_of_brave_ulysses.mp3",
                TrackVisualPath =
                    "/src/backend/Uploads/Pictures/TrackVisuals/e4c4d81a-cbd5-4087-acc5-f23a4cb6315a_tales_of_brave_ulysses.jpg",
                UserId = _userId,
                AlbumId = _disraeliGearsAlbumId, // Disraeli Gears
                FirstGenreId = _rockGenreId, // Rock
                SecondaryGenreId = new Guid("b1538091-1957-401b-932b-cef92e05654f"),
                BlockchainHashId = null,
                ReleaseDate = DateTime.Now,
            }
        );

        await dbContext.SaveChangesAsync();
    }
  
    private static async Task SeedArtists(ApplicationDbContext dbContext)
    {
        await dbContext.Artists.AddRangeAsync(
            new Artist
            {
                Id = new Guid("332b8af0-e2af-41ac-a2ad-4bafdf26492e"),
                Firstname = "Nick",
                Lastname = "Mason",
            },
            new Artist
            {
                Id = new Guid("a0da1f96-4cdf-4490-8f70-be07741229a3"),
                Firstname = "David",
                Lastname = "Gilmour",
            },
            new Artist
            {
                Id = new Guid("9cefa4a8-ac72-4551-bb2c-14b4c2aaa4ed"),
                Firstname = "Roger",
                Lastname = "Waters",
            },
            new Artist
            {
                Id = new Guid("3e235143-6286-4c61-9190-dcdb4f3aab23"),
                Firstname = "Richard",
                Lastname = "Wright",
            },
            new Artist
            {
                Id = new Guid("0a4eebf1-9578-4c8b-95d4-98a3857543c4"),
                Firstname = "Syd",
                Lastname = "Barrett",
            },
            new Artist
            {
                Id = new Guid("acc6c5a9-cdd9-4841-8c02-dc56e4e6d6b6"),
                Firstname = "Eric",
                Lastname = "Clapton",
            },
            new Artist
            {
                Id = new Guid("30800195-feb4-472d-8b9d-842c8e8330ee"),
                Firstname = "Jack",
                Lastname = "Bruce",
            }
        );

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedTrackArtistContributions(ApplicationDbContext dbContext)
    {
        // Récupérer les IDs des tracks
        var speakToMeId = new Guid("8b74701f-2636-42a0-af8e-ff353169a6c2");
        var breatheId = new Guid("d564f3a2-4e56-47aa-82a7-d3e2c4b5d6f7");
        var onTheRunId = new Guid("11232dcf-2f55-41d2-86f5-07026989e827");
        var timeId = new Guid("d23d9b8e-50ff-4322-b454-00539e009aa9");
        var wishYouWereHereId = new Guid("12fba9b9-43de-41fa-9086-5ce2a34cb971");
        var comfortablyNumbId = new Guid("a4037469-38b4-4fb5-81ab-79049e16de19");
        var heyYouId = new Guid("385dcdba-b43b-4f46-8d04-51da0b863b1d");
        var sunshineOfYourLoveId = new Guid("feb3ccab-ddfe-4e32-92ae-9f34d107c82f");
        var whiteRoomId = new Guid("1c4611b8-0483-48af-8a47-eb57215cc6f1");
        var talesOfBraveUlyssesId = new Guid("e4c4d81a-cbd5-4087-acc5-f23a4cb6315a");
        var anotherBrickInTheWallId = new Guid("ff3f70d3-3dbf-4e72-b44f-94147378bbe6");

        // IDs des artistes tels que définis dans SeedArtists
        var nickMasonId = new Guid("332b8af0-e2af-41ac-a2ad-4bafdf26492e");
        var davidGilmourId = new Guid("a0da1f96-4cdf-4490-8f70-be07741229a3");
        var rogerWatersId = new Guid("9cefa4a8-ac72-4551-bb2c-14b4c2aaa4ed");
        var richardWrightId = new Guid("3e235143-6286-4c61-9190-dcdb4f3aab23");
        var sydBarrettId = new Guid("0a4eebf1-9578-4c8b-95d4-98a3857543c4");
        var ericClaptonId = new Guid("acc6c5a9-cdd9-4841-8c02-dc56e4e6d6b6");
        var jackBruceId = new Guid("30800195-feb4-472d-8b9d-842c8e8330ee");

        // Créer les associations track-artist-contribution
        await dbContext.TrackArtistContributions.AddRangeAsync(
            // Nick Mason - Speak To Me - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = speakToMeId,
                ArtistId = nickMasonId,
                ContributionId = _musiqueContributionId
            },
            // David Gilmour - Speak To Me - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = speakToMeId,
                ArtistId = davidGilmourId,
                ContributionId = _musiqueContributionId
            },
            // Roger Waters - On the Run - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = onTheRunId,
                ArtistId = rogerWatersId,
                ContributionId = _musiqueContributionId
            },
            // Richard Wright - Time - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = timeId,
                ArtistId = richardWrightId,
                ContributionId = _musiqueContributionId
            },
            // Syd Barrett - Wish You Were Here - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = wishYouWereHereId,
                ArtistId = sydBarrettId,
                ContributionId = _musiqueContributionId
            },
            // Roger Waters - Comfortably Numb - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = comfortablyNumbId,
                ArtistId = rogerWatersId,
                ContributionId = _musiqueContributionId
            },
            // David Gilmour - Hey You - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = heyYouId,
                ArtistId = davidGilmourId,
                ContributionId = _musiqueContributionId
            },
            // Eric Clapton - Sunshine of Your Love - Paroles
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = sunshineOfYourLoveId,
                ArtistId = ericClaptonId,
                ContributionId = _parolesContributionId
            },
            // Jack Bruce - White Room - Paroles
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = whiteRoomId,
                ArtistId = jackBruceId,
                ContributionId = _parolesContributionId
            },
            // Eric Clapton - White Room - Paroles
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = whiteRoomId,
                ArtistId = ericClaptonId,
                ContributionId = _parolesContributionId
            },
            // Roger Waters - Another Brick in the Wall, Pt. 2 - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = anotherBrickInTheWallId,
                ArtistId = rogerWatersId,
                ContributionId = _musiqueContributionId
            },
            // Roger Waters - Breathe - Paroles
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = breatheId,
                ArtistId = rogerWatersId,
                ContributionId = _parolesContributionId
            },
            // Eric Clapton - Tales of Brave Ulysses - Musique
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = talesOfBraveUlyssesId,
                ArtistId = ericClaptonId,
                ContributionId = _musiqueContributionId
            },
            // Roger Waters - Another Brick in the Wall, Pt. 2 - Paroles
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = anotherBrickInTheWallId,
                ArtistId = rogerWatersId,
                ContributionId = _parolesContributionId
            },
            // David Gilmour - Comfortably Numb - Musique et paroles
            new TrackArtistContribution
            {
                Id = Guid.NewGuid(),
                TrackId = comfortablyNumbId,
                ArtistId = davidGilmourId,
                ContributionId = _musiqueEtParolesContributionId
            }
        );

        await dbContext.SaveChangesAsync();
    }


    private static async Task SeedUserInfos(ApplicationDbContext dbContext)
    {
        await dbContext.UserInfos.AddRangeAsync(
            new UserInfo
            {
                Id = new Guid("d8f71222-4d29-4ff4-925a-68a308552c71"),
                PhoneNumber = "0123456789",
                Birthdate = new DateTime(1996, 05, 14, 0, 0, 0, DateTimeKind.Utc),
                ProfilePicturePath = null,
                Address = "1 rue de la Paix",
                City = "Paris",
                Country = "France",
                PostalCode = "75000",
                CreationDate = new DateTime(2024, 05, 14, 0, 0, 0, DateTimeKind.Utc),
                UpdateDate = null,
            }
        );

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedAppUsers(ApplicationDbContext dbContext)
    {
        await dbContext.Users.AddRangeAsync(
            new AppUser
            {
                Id = _userId,
                FirstName = "Pierre",
                LastName = "Fraisse",
                UserName = "p.fraisse@mail.com",
                NormalizedUserName = "P.FRAISSE@MAIL.COM",
                Email = "p.fraisse@mail.com",
                NormalizedEmail = "P.FRAISSE@MAIL.COM",
                EmailConfirmed = false,
                PasswordHash =
                    "AQAAAAIAAYagAAAAEHIpnPV0RitSgXYLnB/w9OAc0ER79cE0ZGHRiIIPIH3a1tnKIYpBGtOod+TMJsYcEg==",
                SecurityStamp = "7F4FEWBMI62NF6VRYEGZBOYBBUJMILQK",
                ConcurrencyStamp = "a2494767-7420-4216-b433-47619c6818b1",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                UserInfoId = new Guid("d8f71222-4d29-4ff4-925a-68a308552c71"),
            }
        );

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedUserRoles(ApplicationDbContext dbContext)
    {
        await dbContext.UserRoles.AddRangeAsync(
            new IdentityUserRole<string> { UserId = _userId, RoleId = "1" }
        );

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedRoles(ApplicationDbContext dbContext)
    {
        var roles = new List<IdentityRole>
    {
        new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
        new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
    };

        foreach (var role in roles)
        {
            var roleExists = await dbContext.Roles.AnyAsync(r => r.Name == role.Name);
            if (!roleExists)
            {
                await dbContext.Roles.AddAsync(role);
            }
        }

        await dbContext.SaveChangesAsync();
    }

}
