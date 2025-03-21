// Copyright : Pierre FRAISSE
// backend>backendTests>ContributionServiceTests.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.ContributionDTOs;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.Services;
using Moq;

namespace backendTests.ServiceTests;

[Collection("ServiceTests")]
public class ContributionServiceTests
{
    private readonly Mock<IContributionRepository> _mockContributionRepository;
    private readonly Mock<IArtistRepository> _mockArtistRepository;
    private readonly ContributionService _contributionService;

    public ContributionServiceTests()
    {
        _mockContributionRepository = new Mock<IContributionRepository>();
        _mockArtistRepository = new Mock<IArtistRepository>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Contribution, ContributionDto>().ReverseMap();
            cfg.CreateMap<ContributionDto, Contribution>()
                .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
        });
        var mapper = mapperConfig.CreateMapper();
        _contributionService = new ContributionService(
            _mockContributionRepository.Object,
            _mockArtistRepository.Object,
            mapper
        );
    }

    /********* Getters *********/
    [Fact]
    public async Task GetListAsync_ShouldReturnListOfContributions()
    {
        // Arrange
        var contributionList = new List<Contribution>
        {
            new Contribution
            {
                Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
                Label = "Test",
            },
            new Contribution
            {
                Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
                Label = "Test",
            },
        };
        _mockContributionRepository
            .Setup(repo => repo.GetListAsync())
            .ReturnsAsync(contributionList);

        // Act
        var expectedList = await _contributionService.GetListAsync();

        // Assert
        Assert.Equal(contributionList.Count, expectedList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnContribution_IfIdIsValid()
    {
        // Arrange
        var contribution = new Contribution
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };
        _mockContributionRepository
            .Setup(repo => repo.GetByIdAsync(contribution.Id))
            .ReturnsAsync(contribution);

        // Act
        var expectedContribution = await _contributionService.GetByIdAsync(contribution.Id);

        // Assert
        if (expectedContribution != null)
            Assert.Equal(contribution.Id, expectedContribution.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_IfIdIsInvalid()
    {
        // Arrange

        // Act
        var expectedContribution = await _contributionService.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(expectedContribution);
    }

    [Fact]
    public async Task GetByArtistIdAsync_ShouldReturnContribution_IfArtistIdIsValid()
    {
        // Arrange
        var artist = new Artist
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            ContributionId = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
        };

        var contribution = new Contribution
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        _mockArtistRepository.Setup(repo => repo.GetByIdAsync(artist.Id)).ReturnsAsync(artist);
        _mockContributionRepository
            .Setup(repo => repo.GetByIdAsync(artist.ContributionId))
            .ReturnsAsync(contribution);

        // Act
        var expectedContribution = await _contributionService.GetByArtistIdAsync(artist.Id);

        // Assert
        if (expectedContribution != null)
            Assert.Equal(contribution.Id, expectedContribution.Id);
    }

    [Fact]
    public async Task GetByArtistIdAsync_ShouldReturnNull_IfArtistIdIsInvalid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var artist = new Artist
        {
            Id = id,
            ContributionId = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
        };

        _mockArtistRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(artist);
        _mockContributionRepository
            .Setup(repo => repo.GetByIdAsync(artist.ContributionId))
            .ReturnsAsync((Contribution?)null);

        // Act
        var contribution = await _contributionService.GetByArtistIdAsync(id);

        // Assert
        Assert.Null(contribution);
    }

    [Fact]
    public async Task GetByArtistIdAsync_ShouldThrowException_IfArtistIsNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _mockArtistRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Artist?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _contributionService.GetByArtistIdAsync(id)
        );
    }

    /********* Create *********/

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedContribution_IfContributionIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var contributionDto = new ContributionDto { Label = "Test" };

        var contribution = new Contribution { Id = id, Label = "Test" };

        _mockContributionRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<Contribution>()))
            .ReturnsAsync(contribution);

        // Act
        var expectedContribution = await _contributionService.CreateAsync(contributionDto);

        // Assert
        Assert.Equal(contribution.Id, id);
        Assert.Equal(contribution.Label, expectedContribution?.Label);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnNull_IfContributionIsNull()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        var contribution = new Contribution();
        _mockContributionRepository
            .Setup(x => x.CreateAsync(contribution))
            .ReturnsAsync((Contribution?)null);

        // Act
        var expectedContribution = await _contributionService.CreateAsync(contributionDto);

        // Assert
        Assert.Null(expectedContribution);
    }

    /********* Update *********/

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedContribution_IfContributionIsValid()
    {
        // Arrange
        var contribution = new Contribution
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        var newContribution = new ContributionDto() { Label = "Test" };

        _mockContributionRepository
            .Setup(repo => repo.GetByIdAsync(contribution.Id))
            .ReturnsAsync(contribution);
        _mockContributionRepository
            .Setup(repo => repo.UpdateAsync(new Contribution()))
            .ReturnsAsync(contribution);

        // Act
        var expectedContribution = await _contributionService.UpdateAsync(
            contribution.Id,
            newContribution
        );

        // Assert
        if (expectedContribution != null)
        {
            Assert.Equal(contribution.Id, expectedContribution.Id);
            Assert.Equal(contribution.Label, expectedContribution.Label);
        }
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_IfContributionIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var contribution = new ContributionDto { Label = "Test" };

        _mockContributionRepository
            .Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync((Contribution?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _contributionService.UpdateAsync(id, contribution)
        );
    }

    /********* Delete *********/

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfContributionIsValid()
    {
        // Arrange
        var contribution = new Contribution
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        _mockContributionRepository
            .Setup(repo => repo.GetByIdAsync(contribution.Id))
            .ReturnsAsync(contribution);
        _mockContributionRepository
            .Setup(repo => repo.DeleteAsync(contribution.Id))
            .ReturnsAsync(true);

        // Act
        var expectedContribution = await _contributionService.DeleteAsync(contribution.Id);

        // Assert
        Assert.True(expectedContribution);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_IfContributionIsNull()
    {
        // Arrange
        var contribution = new Contribution
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        _mockContributionRepository
            .Setup(repo => repo.GetByIdAsync(contribution.Id))
            .ReturnsAsync((Contribution?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _contributionService.DeleteAsync(contribution.Id)
        );
    }
}
