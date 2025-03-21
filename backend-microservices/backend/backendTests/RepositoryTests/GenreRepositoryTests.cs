// Copyright : Pierre FRAISSE
// backend>backendTests>GenreRepositoryTests.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backendTests => GenreRepositoryTests.cs
// Created : 2024/01/09 - 16:43
// Updated : 2024/01/09 - 16:43

using backendTests.TestServices.Interface;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendTests.RepositoryTests;

[Collection("RepositoryTests")]
public class GenreRepositoryTests(ITestApplicationDbContext testApplicationDbContext)
{
    private readonly ApplicationDbContext _context = testApplicationDbContext.GetContext();

    /*********** Getters ***********/
    [Fact]
    public async Task GetListAsync_ShouldReturnGenreList()
    {
        // Arrange
        var repository = new GenreRepository(_context);

        // Act
        var genreList = await repository.GetListAsync();
        var trackedEntities = _context.ChangeTracker.Entries<Genre>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(genreList);
        Assert.Equal(4, genreList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnGenre_IfIdIsValid()
    {
        // Arrange
        var repository = new GenreRepository(_context);
        var genreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7");

        // Act
        var genre = await repository.GetByIdAsync(genreId);
        var trackedEntities = _context.ChangeTracker.Entries<Genre>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(genre);
        Assert.Equal(genreId, genre.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenGenreIdIsInvalid()
    {
        // Arrange
        GenreRepository repository = new GenreRepository(_context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(
            async () =>
                await repository.GetByIdAsync(new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f8"))
        );
    }

    /*********** Create ***********/
    [Fact]
    public async Task CreateAsync_ShouldReturnGenre_IfGenreIsValid()
    {
        // Arrange
        var repository = new GenreRepository(_context);
        var genre = new Genre
        {
            Id = new Guid("f270f6b3-256f-409d-b547-2a1972937c98"),
            Label = "Producteur",
        };

        // Act
        var createdGenre = await repository.CreateAsync(genre);
        var trackedEntities = _context.ChangeTracker.Entries<Genre>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(createdGenre);
        Assert.Equal(genre.Id, createdGenre.Id);
    }

    /*********** Update ***********/
    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedGenre_IfGenreIsValid()
    {
        // Arrange
        var repository = new GenreRepository(_context);
        var newGenre = new Genre
        {
            Id = new Guid("028450d1-783e-45c6-a59f-0241ced8731c"),
            Label = "Techno",
        };

        // Act
        var updatedGenre = await repository.UpdateAsync(newGenre);
        var trackedEntities = _context.ChangeTracker.Entries<Genre>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(updatedGenre);
        Assert.Equal(newGenre.Id, updatedGenre.Id);
        Assert.Equal(newGenre.Label, updatedGenre.Label);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowArgumentException_IfGenreIsInvalid()
    {
        // Arrange
        var repository = new GenreRepository(_context);
        var newGenre = new Genre
        {
            Id = new Guid("8587efe2-04fb-4c82-83f9-a5a0813361f4"),
            Label = "Techno",
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => repository.UpdateAsync(newGenre));
    }

    /*********** Delete ***********/
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfGenreIsValid()
    {
        // Arrange
        var repository = new GenreRepository(_context);
        var genreId = new Guid("028450d1-783e-45c6-a59f-0241ced8731c");

        // Act
        var isDeleted = await repository.DeleteAsync(genreId);
        var trackedEntities = _context.ChangeTracker.Entries<Genre>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.True(isDeleted);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_IfGenreIsInvalid()
    {
        // Arrange
        var repository = new GenreRepository(_context);
        var genreId = new Guid("8587efe2-04fb-4c82-83f9-a5a0813361f4");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await repository.DeleteAsync(genreId)
        );
        Assert.Equal($"No entity with the given id: {genreId}", exception.Message);
        var trackedEntities = _context.ChangeTracker.Entries<Genre>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }
    }
}
