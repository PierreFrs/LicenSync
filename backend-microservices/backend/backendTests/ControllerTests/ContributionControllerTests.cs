// Copyright : Pierre FRAISSE
// backend>backendTests>ContributionControllerTests.cs
// Created : 2024/05/1414 - 13:05

using API.Controllers;
using Core.DTOs.ContributionDTOs;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace backendTests.ControllerTests;

[Collection("ControllerTests")]
public class ContributionControllerTests
{
    private readonly Mock<IContributionService> _mockContributionService;
    private readonly ContributionController _contributionController;

    public ContributionControllerTests()
    {
        _mockContributionService = new Mock<IContributionService>();
        _contributionController = new ContributionController(_mockContributionService.Object);
    }

    /********** Getters **********/
    [Fact]
    public async Task Get_ReturnsOk()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        var contributionDtoList = new List<ContributionDto> { contributionDto };
        _mockContributionService.Setup(x => x.GetListAsync()).ReturnsAsync(contributionDtoList);

        // Act
        var result = await _contributionController.Get();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Get_ReturnsListOfContributions()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        var contributionDtoList = new List<ContributionDto> { contributionDto };
        _mockContributionService.Setup(x => x.GetListAsync()).ReturnsAsync(contributionDtoList);

        // Act
        var result = await _contributionController.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<List<ContributionDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsOk_IfIdIsValid()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        _mockContributionService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contributionDto);

        // Act
        var result = await _contributionController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_ReturnsContribution_IfIdIsValid()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        _mockContributionService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contributionDto);

        // Act
        var result = await _contributionController.GetById(It.IsAny<Guid>());

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<ContributionDto>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_IfIdIsInvalid()
    {
        // Arrange
        _mockContributionService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ContributionDto?)null);

        // Act
        var result = await _contributionController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetByArtistId_ReturnsOk_IfIdIsValid()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        _mockContributionService
            .Setup(x => x.GetByArtistIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contributionDto);

        // Act
        var result = await _contributionController.GetByArtistId(It.IsAny<Guid>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetByArtistId_ReturnsContribution_IfIdIsValid()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        _mockContributionService
            .Setup(x => x.GetByArtistIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contributionDto);

        // Act
        var result = await _contributionController.GetByArtistId(It.IsAny<Guid>());

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<ContributionDto>(okResult.Value);
    }

    [Fact]
    public async Task GetByArtistId_ReturnsNotFound_IfIdIsInvalid()
    {
        // Arrange
        _mockContributionService
            .Setup(x => x.GetByArtistIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ContributionDto?)null);

        // Act
        var result = await _contributionController.GetByArtistId(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    /********** Create **********/
    [Fact]
    public async Task Create_ReturnsOk_IfContributionIsValid()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        _mockContributionService
            .Setup(x => x.CreateAsync(It.IsAny<ContributionDto>()))
            .ReturnsAsync(contributionDto);

        // Act
        var result = await _contributionController.Create(contributionDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsContribution_IfContributionIsValid()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        _mockContributionService
            .Setup(x => x.CreateAsync(It.IsAny<ContributionDto>()))
            .ReturnsAsync(contributionDto);

        // Act
        var result = await _contributionController.Create(contributionDto);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<ContributionDto>(okResult.Value);
    }

    [Fact]
    public async Task Create_ThrowsException_IfContributionIsNull()
    {
        // Arrange
        _mockContributionService
            .Setup(x => x.CreateAsync(It.IsAny<ContributionDto>()))
            .ReturnsAsync((ContributionDto?)null);

        // Act

        // Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _contributionController.Create(It.IsAny<ContributionDto>())
        );
        Assert.Equal("Something went wrong with the contribution creation", exception.Message);
    }

    /********** Update **********/
    [Fact]
    public async Task Update_ReturnsOk_IfContributionIsValid()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        var contributionDtoUpdate = new ContributionDto();

        _mockContributionService
            .Setup(x => x.UpdateAsync(It.IsAny<Guid>(), contributionDtoUpdate))
            .ReturnsAsync(contributionDto);

        // Act
        var result = await _contributionController.Update(It.IsAny<Guid>(), contributionDtoUpdate);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsContribution_IfContributionIsValid()
    {
        // Arrange
        var contributionDto = new ContributionDto();
        var contributionDtoUpdate = new ContributionDto();

        _mockContributionService
            .Setup(x => x.UpdateAsync(It.IsAny<Guid>(), contributionDtoUpdate))
            .ReturnsAsync(contributionDto);

        // Act
        var result = await _contributionController.Update(It.IsAny<Guid>(), contributionDtoUpdate);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<ContributionDto>(okResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_IfContributionIsNull()
    {
        // Arrange
        var contributionDtoUpdate = new ContributionDto();
        _mockContributionService
            .Setup(x => x.UpdateAsync(It.IsAny<Guid>(), contributionDtoUpdate))
            .ReturnsAsync((ContributionDto?)null);

        // Act
        var result = await _contributionController.Update(It.IsAny<Guid>(), contributionDtoUpdate);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    /********** Delete **********/
    [Fact]
    public async Task Delete_ReturnsOk_IfContributionIsValid()
    {
        // Arrange
        ContributionDto contributionDto = new ContributionDto();
        _mockContributionService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contributionDto);
        _mockContributionService.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        // Act
        var result = await _contributionController.Delete(It.IsAny<Guid>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_IfContributionIsNull()
    {
        // Arrange
        _mockContributionService.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        var result = await _contributionController.Delete(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ThrowsException_IfDeletionFails()
    {
        // Arrange
        ContributionDto contributionDto = new ContributionDto();
        _mockContributionService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contributionDto);
        _mockContributionService.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act

        // Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _contributionController.Delete(It.IsAny<Guid>())
        );
        Assert.Equal("Something went wrong with the contribution deletion", exception.Message);
    }
}
