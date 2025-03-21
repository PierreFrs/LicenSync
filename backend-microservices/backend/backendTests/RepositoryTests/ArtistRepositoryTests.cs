// Copyright : Pierre FRAISSE
// backend>backendTests>ArtistRepositoryTests.cs
// Created : 2024/05/1414 - 13:05

using backendTests.TestServices.Interface;
using Core.Entities;
using Core.Specifications;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendTests.RepositoryTests;

[Collection("RepositoryTests")]
public class ArtistRepositoryTests(ITestApplicationDbContext testApplicationDbContext)
{
    private readonly ApplicationDbContext _context = testApplicationDbContext.GetContext();

    /*********** Getters ***********/
    [Fact]
    public async Task GetListAsync_ShouldReturnArtistList()
    {
        // Arrange
        var repository = new ArtistRepository(_context);

        // Act
        var artistList = await repository.GetListAsync();
        var trackedEntities = _context.ChangeTracker.Entries<Artist>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(artistList);
        Assert.Equal(3, artistList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnArtist_IfIdIsValid()
    {
        // Arrange
        var repository = new ArtistRepository(_context);
        var artistId = new Guid("c1b0f619-f02d-4d31-823b-465cfb01cae4");

        // Act
        var artist = await repository.GetByIdAsync(artistId);
        var trackedEntities = _context.ChangeTracker.Entries<Artist>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(artist);
        Assert.Equal(artistId, artist.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenArtistIdIsInvalid()
    {
        // Arrange
        ArtistRepository repository = new ArtistRepository(_context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(
            async () =>
                await repository.GetByIdAsync(new Guid("46b0f619-f02d-4d31-823b-465cfb01cae3"))
        );
    }

    [Fact]
    public async Task GetArtistsByTrackIdAsync_ShouldReturnArtistList_IfTrackIdIsValid()
    {
        // Arrange
        var repository = new ArtistRepository(_context);
        var trackId = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327");

        // Act
        var artistList = await repository.GetEntityListBySpecificationAsync(
            new ArtistSpecification(trackId)
        );
        var trackedEntities = _context.ChangeTracker.Entries<Artist>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(artistList);
        Assert.Single(artistList);
    }

    /*********** Create ***********/
    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedArtist_IfArtistIsValid()
    {
        // Arrange
        var repository = new ArtistRepository(_context);
        var artist = new Artist() { Firstname = "Test", Lastname = "Test" };

        // Act
        var createdArtist = await repository.CreateAsync(artist);
        var trackedEntities = _context.ChangeTracker.Entries<Artist>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(createdArtist);
        Assert.Equal(artist.Firstname, createdArtist.Firstname);
        Assert.Equal(artist.Lastname, createdArtist.Lastname);
    }

    /*********** Updaters ***********/
    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedArtist_IfArtistIsValid()
    {
        // Arrange
        var repository = new ArtistRepository(_context);
        var artist = new Artist()
        {
            Id = new Guid("1ecf2c42-9497-4977-b846-9c1d427f83a0"),
            Firstname = "Liam",
            Lastname = "Gallagher",
        };

        // Act
        var updatedArtist = await repository.UpdateAsync(artist);
        var trackedEntities = _context.ChangeTracker.Entries<Artist>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(updatedArtist);
        Assert.Equal(artist.Firstname, updatedArtist.Firstname);
        Assert.Equal(artist.Lastname, updatedArtist.Lastname);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_IfArtistIsNull()
    {
        // Arrange
        var repository = new ArtistRepository(_context);
        var artist = new Artist()
        {
            Id = new Guid("46b0f619-f02d-4d31-823b-465cfb01cae3"),
            Firstname = "Liam",
            Lastname = "Gallagher",
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => repository.UpdateAsync(artist));
    }

    /*********** Delete ***********/
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfArtistIsDeleted()
    {
        // Arrange
        var repository = new ArtistRepository(_context);
        var artistId = new Guid("1ecf2c42-9497-4977-b846-9c1d427f83a9");
        var artist = new Artist()
        {
            Id = artistId,
            Firstname = "Liam",
            Lastname = "Gallagher",
        };
        _context.Add(artist);
        await _context.SaveChangesAsync();

        // Act
        var isDeleted = await repository.DeleteAsync(artistId);
        var trackedEntities = _context.ChangeTracker.Entries<Artist>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.True(isDeleted);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_IfArtistIsNull()
    {
        // Arrange
        var repository = new ArtistRepository(_context);
        var artistId = new Guid("46b0f619-f02d-4d31-823b-465cfb01cae3");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await repository.DeleteAsync(artistId)
        );
        Assert.Equal($"No entity with the given id: {artistId}", exception.Message);
        var trackedEntities = _context.ChangeTracker.Entries<Artist>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }
    }
}
