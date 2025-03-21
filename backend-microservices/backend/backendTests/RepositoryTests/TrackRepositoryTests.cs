// Copyright : Pierre FRAISSE
// backend>backendTests>TrackRepositoryTests.cs
// Created : 2024/05/1414 - 13:05

using backendTests.TestServices.Interface;
using Core.Entities;
using Core.Specifications;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendTests.RepositoryTests;

[Collection("RepositoryTests")]
public class TrackRepositoryTests(ITestApplicationDbContext testApplicationDbContext)
{
    private readonly ApplicationDbContext _context = testApplicationDbContext.GetContext();

    /*********** Getters ***********/
    [Fact]
    public async Task GetListAsync_ShouldReturnTrackList()
    {
        // Arrange
        var repository = new TrackRepository(_context);

        // Act
        var trackList = await repository.GetListAsync();
        var trackedEntities = _context.ChangeTracker.Entries<Track>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(trackList);
        Assert.Equal(3, trackList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTrack_ifIdIsValid()
    {
        // Arrange
        var repository = new TrackRepository(_context);

        // Act
        var expectedTrack = await repository.GetByIdAsync(
            new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327")
        );
        var trackedEntities = _context.ChangeTracker.Entries<Track>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        //Assert
        Assert.NotNull(expectedTrack);
        Assert.Equal(new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"), expectedTrack.Id);
        Assert.Equal("white_room.mp3", expectedTrack.TrackTitle);
        Assert.Equal("Polydor", expectedTrack.RecordLabel);
        Assert.Equal("03:04:00", expectedTrack.Length);
        Assert.Equal("/test/file/path/white_room.mp3", expectedTrack.AudioFilePath);
        Assert.Equal("/test/file/path/white_room_visual.jpg", expectedTrack.TrackVisualPath);
        Assert.Equal(new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"), expectedTrack.FirstGenreId);
        Assert.Equal(
            new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
            expectedTrack.SecondaryGenreId
        );
        Assert.Equal(new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"), expectedTrack.AlbumId);
        Assert.Equal("46b0f619-f02d-4d31-823b-465cfb01cae4", expectedTrack.UserId);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenTrackIdIsInvalid()
    {
        // Arrange
        TrackRepository repository = new TrackRepository(_context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(
            async () =>
                await repository.GetByIdAsync(new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f8"))
        );
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnTrackList_IfUserIdIsValid()
    {
        // Arrange
        var repository = new TrackRepository(_context);

        // Act
        var expectedTrackList = await repository.GetEntityListBySpecificationAsync(
            new TrackSpecification("46b0f619-f02d-4d31-823b-465cfb01cae4")
        );
        var trackedEntities = _context.ChangeTracker.Entries<Track>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        //Assert
        Assert.NotNull(expectedTrackList);
        Assert.Single(expectedTrackList);
        Assert.Equal(new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"), expectedTrackList[0].Id);
        Assert.Equal("white_room.mp3", expectedTrackList[0].TrackTitle);
        Assert.Equal("Polydor", expectedTrackList[0].RecordLabel);
        Assert.Equal("03:04:00", expectedTrackList[0].Length);
        Assert.Equal("/test/file/path/white_room.mp3", expectedTrackList[0].AudioFilePath);
        Assert.Equal("/test/file/path/white_room_visual.jpg", expectedTrackList[0].TrackVisualPath);
        Assert.Equal(
            new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
            expectedTrackList[0].FirstGenreId
        );
        Assert.Equal(
            new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
            expectedTrackList[0].SecondaryGenreId
        );
        Assert.Equal(
            new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
            expectedTrackList[0].AlbumId
        );
        Assert.Equal("46b0f619-f02d-4d31-823b-465cfb01cae4", expectedTrackList[0].UserId);
    }

    /*********** Create ***********/
    [Fact]
    public async Task CreateCategoryAsync_ShouldAddTrack_IfDataIsValid()
    {
        // Arrange
        var repository = new TrackRepository(_context);
        var newTrack = new Track
        {
            Id = new Guid("9f8820f2-dc10-40a7-b404-e16c0facaddf"),
            TrackTitle = "pump_up_the_jam.mp3",
            RecordLabel = "Swanyard_Records",
            Length = "03:39:00",
            AudioFilePath = "/test/file/path/pump_up_the_jam.mp3",
            TrackVisualPath = "/test/file/path/pump_up_the_jam_visual.jpg",
            FirstGenreId = new Guid("7efab38d-e3f1-4299-891e-21593e91a461"),
            SecondaryGenreId = new Guid("ab624391-b0df-429e-918d-21848926bdba"),
            AlbumId = new Guid("d9f45f31-eb53-467c-a643-9c013c253a40"),
            UserId = "ea9af77e-d3b6-452f-9c18-278248b249ff",
        };

        var trackList = await repository.GetListAsync();

        // Act
        var expectedTrack = await repository.CreateAsync(newTrack);
        var updatedTrackList = await repository.GetListAsync();
        var trackedEntities = _context.ChangeTracker.Entries<Track>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(expectedTrack);
        Assert.Equal(updatedTrackList.Count, trackList.Count + 1);
        Assert.Equal(new Guid("9f8820f2-dc10-40a7-b404-e16c0facaddf"), expectedTrack.Id);
        Assert.Equal("pump_up_the_jam.mp3", expectedTrack.TrackTitle);
        Assert.Equal("Swanyard_Records", expectedTrack.RecordLabel);
        Assert.Equal("03:39:00", expectedTrack.Length);
        Assert.Equal("/test/file/path/pump_up_the_jam.mp3", expectedTrack.AudioFilePath);
        Assert.Equal("/test/file/path/pump_up_the_jam_visual.jpg", expectedTrack.TrackVisualPath);
        Assert.Equal(new Guid("7efab38d-e3f1-4299-891e-21593e91a461"), expectedTrack.FirstGenreId);
        Assert.Equal(
            new Guid("ab624391-b0df-429e-918d-21848926bdba"),
            expectedTrack.SecondaryGenreId
        );
        Assert.Equal(new Guid("d9f45f31-eb53-467c-a643-9c013c253a40"), expectedTrack.AlbumId);
        Assert.Equal("ea9af77e-d3b6-452f-9c18-278248b249ff", expectedTrack.UserId);
    }

    /*********** Update ***********/
    [Fact]
    public async Task StoreHashGuidInDatabaseAsync_ShouldReturnTrack_IfDataIsValid()
    {
        // Arrange
        var repository = new TrackRepository(_context);
        var trackId = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327");
        var hashId = new Guid("c1b0f619-f02d-4d31-823b-465cfb01cae4");

        // Act
        var updatedTrack = await repository.StoreHashGuidInDatabaseAsync(trackId, hashId);
        var trackedEntities = _context.ChangeTracker.Entries<Track>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(updatedTrack);
        Assert.Equal(new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"), updatedTrack.Id);
        Assert.Equal(
            new Guid("c1b0f619-f02d-4d31-823b-465cfb01cae4"),
            updatedTrack.BlockchainHashId
        );
    }

    [Fact]
    public async Task StoreHashGuidInDatabaseAsync_ShouldThrowArgumentException_IfIdIsInvalid()
    {
        // Arrange
        var repository = new TrackRepository(_context);
        var trackId = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b328");
        var hashId = new Guid("c1b0f619-f02d-4d31-823b-465cfb01cae4");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => repository.StoreHashGuidInDatabaseAsync(trackId, hashId)
        );
    }

    /*********** Delete ***********/
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfSuccessful()
    {
        // Arrange
        var repository = new TrackRepository(_context);
        Guid trackId = new Guid("40a7078d-b96d-41be-9e0c-e53890b32e90");
        var track = new Track()
        {
            Id = trackId,
            TrackTitle = "pump_up_the_jam.mp3",
            RecordLabel = "Swanyard_Records",
            Length = "03:39:00",
            AudioFilePath = "/test/file/path/pump_up_the_jam.mp3",
            TrackVisualPath = "/test/file/path/pump_up_the_jam_visual.jpg",
            FirstGenreId = new Guid("7efab38d-e3f1-4299-891e-21593e91a461"),
            SecondaryGenreId = new Guid("ab624391-b0df-429e-918d-21848926bdba"),
            AlbumId = new Guid("d9f45f31-eb53-467c-a643-9c013c253a40"),
            UserId = "ea9af77e-d3b6-452f-9c18-278248b249ff",
        };

        _context.Tracks.Add(track);
        await _context.SaveChangesAsync();

        // Act
        var deletedTrack = await repository.DeleteAsync(trackId);
        var trackedEntities = _context.ChangeTracker.Entries<Track>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.True(deletedTrack);
        var checkDeleted = await _context.Tracks.FindAsync(trackId);
        Assert.Null(checkDeleted);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowArgumentException_IfIdIsInvalid()
    {
        // Arrange
        var repository = new TrackRepository(_context);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => repository.DeleteAsync(new Guid("40a7078d-b96d-41be-9e0c-e53890b32e93"))
        );
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveTrack_IfIdIsValid()
    {
        // Arrange
        var repository = new TrackRepository(_context);
        var newTrack = new Track
        {
            Id = Guid.NewGuid(),
            TrackTitle = "pump_up_the_jam.mp3",
            RecordLabel = "Swanyard_Records",
            Length = "03:39:00",
            AudioFilePath = "/test/file/path/pump_up_the_jam.mp3",
            TrackVisualPath = "/test/file/path/pump_up_the_jam_visual.jpg",
            FirstGenreId = new Guid("7efab38d-e3f1-4299-891e-21593e91a461"),
            SecondaryGenreId = new Guid("ab624391-b0df-429e-918d-21848926bdba"),
            AlbumId = new Guid("d9f45f31-eb53-467c-a643-9c013c253a40"),
            UserId = "ea9af77e-d3b6-452f-9c18-278248b249ff",
        };
        _context.Tracks.Add(newTrack);
        await _context.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(newTrack.Id);
        var result = await _context.Tracks.FindAsync(newTrack.Id);
        var trackedEntities = _context.ChangeTracker.Entries<Track>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.Null(result);
    }
}
