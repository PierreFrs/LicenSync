// Copyright : Pierre FRAISSE
// backend>backendTests>ContributionRepositoryTests.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backendTests => ContributionRepositoryTests.cs
// Created : 2024/01/09 - 16:08
// Updated : 2024/01/09 - 16:08

using backendTests.TestServices.Interface;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backendTests.RepositoryTests;

[Collection("RepositoryTests")]
public class ContributionRepositoryTests(ITestApplicationDbContext testApplicationDbContext)
{
    private readonly ApplicationDbContext _context = testApplicationDbContext.GetContext();

    /*********** Getters ***********/
    [Fact]
    public async Task GetListAsync_ShouldReturnContributionList()
    {
        // Arrange
        var repository = new ContributionRepository(_context);

        // Act
        var contributionList = await repository.GetListAsync();
        var trackedEntities = _context.ChangeTracker.Entries<Contribution>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(contributionList);
        Assert.Equal(4, contributionList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnContribution_IfIdIsValid()
    {
        // Arrange
        var repository = new ContributionRepository(_context);
        var contributionId = new Guid("243584bf-c430-4525-a719-52f1fcd41241");

        // Act
        var contribution = await repository.GetByIdAsync(contributionId);
        var trackedEntities = _context.ChangeTracker.Entries<Contribution>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(contribution);
        Assert.Equal(contributionId, contribution.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenContributionIdIsInvalid()
    {
        // Arrange
        ContributionRepository repository = new ContributionRepository(_context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(
            async () =>
                await repository.GetByIdAsync(new Guid("5e9a939f-598f-4372-b444-936abbf40d97"))
        );
    }

    /*********** Create ***********/
    [Fact]
    public async Task CreateAsync_ShouldReturnContribution_IfContributionIsValid()
    {
        // Arrange
        var repository = new ContributionRepository(_context);
        var contribution = new Contribution
        {
            Id = new Guid("f270f6b3-256f-409d-b547-2a1972937c98"),
            Label = "Producteur",
        };

        // Act
        var createdContribution = await repository.CreateAsync(contribution);
        var trackedEntities = _context.ChangeTracker.Entries<Contribution>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(createdContribution);
        Assert.Equal(contribution.Id, createdContribution.Id);
    }

    /*********** Update ***********/
    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedContribution_IfContributionIsValid()
    {
        // Arrange
        var repository = new ContributionRepository(_context);
        var contribution = new Contribution
        {
            Id = new Guid("243584bf-c430-4525-a719-52f1fcd41241"),
            Label = "Production",
        };

        // Act
        var updatedContribution = await repository.UpdateAsync(contribution);
        var trackedEntities = _context.ChangeTracker.Entries<Contribution>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.NotNull(updatedContribution);
        Assert.Equal(contribution.Id, updatedContribution.Id);
        Assert.Equal(contribution.Label, updatedContribution.Label);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_IfContributionIsInvalid()
    {
        // Arrange
        var repository = new ContributionRepository(_context);
        var contribution = new Contribution
        {
            Id = new Guid("5e9a939f-598f-4372-b444-936abbf40d97"),
            Label = "Production",
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => repository.UpdateAsync(contribution));
    }

    /*********** Delete ***********/
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfContributionIsValid()
    {
        // Arrange
        var repository = new ContributionRepository(_context);
        var contributionId = new Guid("1ecf2c42-9497-4977-b846-9c1d427f83a0");

        // Act
        var isDeleted = await repository.DeleteAsync(contributionId);
        var trackedEntities = _context.ChangeTracker.Entries<Contribution>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }

        // Assert
        Assert.True(isDeleted);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_IfContributionIsInvalid()
    {
        // Arrange
        var repository = new ContributionRepository(_context);
        var contributionId = new Guid("8587efe2-04fb-4c82-83f9-a5a0813361f4");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await repository.DeleteAsync(contributionId)
        );
        Assert.Equal($"No entity with the given id: {contributionId}", exception.Message);
        var trackedEntities = _context.ChangeTracker.Entries<Contribution>();
        foreach (var entity in trackedEntities)
        {
            entity.State = EntityState.Detached;
        }
    }
}
