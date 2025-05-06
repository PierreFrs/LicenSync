// Copyright : Pierre FRAISSE
// backend>backendTests>TestApplicationDbContext.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backendTests => TestDataContext.cs
// Created : 2024/01/08 - 10:27
// Updated : 2024/01/08 - 10:27

using backendTests.TestServices.Interface;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backendTests.TestServices.Implementation;

public class TestApplicationDbContext : ITestApplicationDbContext
{
    private readonly ApplicationDbContext _context;

    public TestApplicationDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "RepositoryTests")
            .Options;
        _context = new ApplicationDbContext(options);
        initDatas();
    }

    public ApplicationDbContext GetContext()
    {
        return _context;
    }

    private void initDatas()
    {
        /********** Users **********/
        var userInfo = new UserInfo
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
        };
        _context.UserInfos.Add(userInfo);

        var appUser = new AppUser
        {
            Id = "46b0f619-f02d-4d31-823b-465cfb01cae4",
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
        };
        _context.Users.Add(appUser);

        /********** Albums **********/
        var album = new Album
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = new string("46b0f619-f02d-4d31-823b-465cfb01cae4"),
        };
        _context.Albums.Add(album);

        album = new Album
        {
            Id = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
            AlbumTitle = "disraeli_gears",
            AlbumVisualPath = "/test/file/path/disraeli_gears",
            UserId = new string("46b0f619-f02d-4d31-823b-465cfb01cae4"),
        };
        _context.Albums.Add(album);

        album = new Album
        {
            Id = new Guid("d2a246c9-6af5-4710-898d-4dfd0a772153"),
            AlbumTitle = "music_for_the_jilted_generation",
            AlbumVisualPath = "/test/file/path/music_for_the_jilted_generation",
            UserId = new string("3ff1781d-6979-4760-8401-8ab29522f9af"),
        };
        _context.Albums.Add(album);

        /********** Artists **********/
        var artist = new Artist
        {
            Id = new Guid("c1b0f619-f02d-4d31-823b-465cfb01cae4"),
            Firstname = "David",
            Lastname = "Bowie",
        };
        _context.Artists.Add(artist);

        artist = new Artist
        {
            Id = new Guid("1f863d4c-849e-490c-865d-e0d2510c93ca"),
            Firstname = "Eric",
            Lastname = "Clapton",
        };
        _context.Artists.Add(artist);

        artist = new Artist
        {
            Id = new Guid("1ecf2c42-9497-4977-b846-9c1d427f83a0"),
            Firstname = "Liam",
            Lastname = "Howlett",
        };
        _context.Artists.Add(artist);

        /********** Contributions **********/
        var contribution = new Contribution
        {
            Id = new Guid("243584bf-c430-4525-a719-52f1fcd41241"),
            Label = "Musique",
        };
        _context.Contributions.Add(contribution);

        contribution = new Contribution
        {
            Id = new Guid("1bca3ff8-6cf8-4d9f-973b-ebcebe713a33"),
            Label = "Paroles",
        };
        _context.Contributions.Add(contribution);

        contribution = new Contribution
        {
            Id = new Guid("1ecf2c42-9497-4977-b846-9c1d427f83a0"),
            Label = "Musique et paroles",
        };
        _context.Contributions.Add(contribution);

        /********** Genres **********/
        var genre = new Genre
        {
            Id = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
            Label = "rock",
        };
        _context.Genres.Add(genre);

        genre = new Genre { Id = new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"), Label = "rap" };
        _context.Genres.Add(genre);

        genre = new Genre
        {
            Id = new Guid("028450d1-783e-45c6-a59f-0241ced8731c"),
            Label = "electro",
        };
        _context.Genres.Add(genre);

        /********** Tracks **********/
        var track = new Track
        {
            Id = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"),
            TrackTitle = "white_room.mp3",
            RecordLabel = "Polydor",
            Length = "03:04:00",
            AudioFilePath = "/test/file/path/white_room.mp3",
            FirstGenreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
            SecondaryGenreId = new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
            AlbumId = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
            UserId = new string("46b0f619-f02d-4d31-823b-465cfb01cae4"),
        };
        _context.Tracks.Add(track);

        track = new Track
        {
            Id = new Guid("20fd521b-f136-4036-83c7-c384769d8e69"),
            TrackTitle = "cream.mp3",
            RecordLabel = "Loud",
            Length = "04:12:00",
            AudioFilePath = "/test/file/path/cream.mp3",
            FirstGenreId = new Guid("028450d1-783e-45c6-a59f-0241ced8731c"),
            SecondaryGenreId = new Guid("e79ff304-68df-46f1-861d-7a969c479fa1"),
            AlbumId = new Guid("d2a246c9-6af5-4710-898d-4dfd0a772153"),
            UserId = new string("3ff1781d-6979-4760-8401-8ab29522f9af"),
        };
        _context.Tracks.Add(track);

        track = new Track
        {
            Id = new Guid("40a7078d-b96d-41be-9e0c-e53890b32e92"),
            TrackTitle = "revolution_909.mp3",
            RecordLabel = "Polydor",
            Length = "03:04:00",
            AudioFilePath = "/test/file/path/revolution_909.mp3",
            FirstGenreId = new Guid("0f18fdad-0097-4a23-a2ab-3394028dbe14"),
            SecondaryGenreId = new Guid("edcf686d-1e34-4198-bc27-c1de84f671ff"),
            AlbumId = new Guid("513bdfff-306e-4afe-9a6a-5ca988d1d12a"),
            UserId = new string("90aa0375-575e-417b-8b8d-62639c8f50df"),
        };
        _context.Tracks.Add(track);

        _context.SaveChanges();
    }
}
