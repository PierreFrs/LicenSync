// Copyright : Pierre FRAISSE
// backend>backendTests>AlbumRepositoryTests.cs
// Created : 2024/05/1414 - 13:05


using backendTests.TestServices.Interface;
using Core.Entities;
using Core.Specifications;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendTests.RepositoryTests;

[Collection("RepositoryTests")]
public class AlbumRepositoryTests(ITestApplicationDbContext testApplicationDbContext)
{
    private readonly ApplicationDbContext _context = testApplicationDbContext.GetContext();

    /*********** Getters ***********/
    [Fact]
    public async Task GetListAsync_ShouldReturnAlbumList()
    {
        // Arrange
        var repository = new AlbumRepository(_context);

        // Act
        var albumList = await repository.GetListAsync();
        var trackedEntities = _context.ChangeTracker.Entries<Album>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(albumList);
        Assert.Equal(3, albumList.Count);
    }

    [Fact]
    public async Task GetEntityListByUserId_ShouldReturnAlbumList_IfUserIdIsValid()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);
        string userId = "46b0f619-f02d-4d31-823b-465cfb01cae4";

        // Act
        var albumList = await repository.GetEntityListBySpecificationAsync(
            new AlbumSpecification(userId)
        );
        var trackedEntities = _context.ChangeTracker.Entries<Album>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(albumList);
        Assert.Equal(3, albumList.Count);
    }

    [Fact]
    public async Task GetEntityListByUserId_ShouldReturnNull_IfUserIdIsInvalid()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);
        string userId = "ad7c9d85-1ef1-459f-8d34-91076009b328";

        // Act
        var albumList = await repository.GetEntityListBySpecificationAsync(
            new AlbumSpecification(userId)
        );
        var trackedEntities = _context.ChangeTracker.Entries<Album>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.Empty(albumList);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAlbum_ifIdIsValid()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);

        // Act
        var expectedAlbum = await repository.GetByIdAsync(
            new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14")
        );
        var trackedEntities = _context.ChangeTracker.Entries<Album>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        //Assert
        Assert.NotNull(expectedAlbum);
        Assert.Equal(new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"), expectedAlbum.Id);
        Assert.Equal("disraeli_gears", expectedAlbum.AlbumTitle);
        Assert.Equal("/test/file/path/disraeli_gears", expectedAlbum.AlbumVisualPath);
        Assert.Equal("46b0f619-f02d-4d31-823b-465cfb01cae4", expectedAlbum.UserId);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenAlbumIdIsInvalid()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(
            async () =>
                await repository.GetByIdAsync(new Guid("ad7c9d85-1ef1-459f-8d34-91076009b328"))
        );
    }

    /*********** Create ***********/
    [Fact]
    public async Task CreateAsync_ShouldCreateAlbum_ifAlbumIsValid()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);
        var album = new Album
        {
            AlbumTitle = "AlbumTest.jpg",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            AlbumVisualPath = "/test/file/path/AlbumTest.jpg",
        };

        // Act
        var expectedAlbum = await repository.CreateAsync(album);
        var trackedEntities = _context.ChangeTracker.Entries<Album>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        //Assert
        Assert.NotNull(expectedAlbum);
        Assert.Equal("AlbumTest.jpg", expectedAlbum.AlbumTitle);
        Assert.Equal("46b0f619-f02d-4d31-823b-465cfb01cae4", expectedAlbum.UserId);
        Assert.Equal("/test/file/path/AlbumTest.jpg", expectedAlbum.AlbumVisualPath);
    }

    /*********** Update ***********/
    [Fact]
    public async Task UpdateAsync_ShouldUpdateAlbum_ifAlbumIsValid()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);
        var updatedAlbum = new Album
        {
            Id = new Guid("d2a246c9-6af5-4710-898d-4dfd0a772153"),
            AlbumTitle = "fresh_cream",
            AlbumVisualPath = "/test/file/path/fresh_cream",
            UserId = "3ff1781d-6979-4760-8401-8ab29522f9af",
        };

        // Act
        var expectedAlbum = await repository.UpdateAsync(updatedAlbum);
        var trackedEntities = _context.ChangeTracker.Entries<Album>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        //Assert
        Assert.NotNull(expectedAlbum);
        Assert.Equal(new Guid("d2a246c9-6af5-4710-898d-4dfd0a772153"), expectedAlbum.Id);
        Assert.Equal("fresh_cream", expectedAlbum.AlbumTitle);
        Assert.Equal("/test/file/path/fresh_cream", expectedAlbum.AlbumVisualPath);
        Assert.Equal("3ff1781d-6979-4760-8401-8ab29522f9af", expectedAlbum.UserId);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_ifExistingAlbumIsNull()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);
        var album = new Album
        {
            Id = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b328"),
            AlbumTitle = "fresh_cream",
            AlbumVisualPath = "/test/file/path/fresh_cream",
            UserId = "3ff1781d-6979-4760-8401-8ab29522f9af",
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => repository.UpdateAsync(album));
    }

    /*********** Delete ***********/
    [Fact]
    public async Task DeleteAsync_ShouldDeleteAlbum_ifAlbumIdIsValid()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);
        var albumId = new Guid("d2a246c9-6af5-4710-898d-4dfd0a772157");
        var album = new Album()
        {
            Id = albumId,
            AlbumTitle = "fresh_cream",
            AlbumVisualPath = "/test/file/path/fresh_cream",
            UserId = "3ff1781d-6979-4760-8401-8ab29522f9af",
        };

        _context.Add(album);
        await _context.SaveChangesAsync();

        // Act
        var isDeleted = await repository.DeleteAsync(albumId);
        var trackedEntities = _context.ChangeTracker.Entries<Album>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        //Assert
        Assert.True(isDeleted);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_ifAlbumIdIsInvalid()
    {
        // Arrange
        AlbumRepository repository = new AlbumRepository(_context);
        var albumId = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b328");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await repository.DeleteAsync(albumId)
        );
        Assert.Equal($"No entity with the given id: {albumId}", exception.Message);
        var trackedEntities = _context.ChangeTracker.Entries<Album>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }
    }
}
