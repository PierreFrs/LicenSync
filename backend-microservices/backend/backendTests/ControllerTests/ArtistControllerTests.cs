// Copyright : Pierre FRAISSE
// backend>backendTests>ArtistControllerTests.cs
// Created : 2024/05/1414 - 13:05

using API.Controllers;
using Core.DTOs.ArtistDTOs;
using Core.DTOs.TrackDTOs;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace backendTests.ControllerTests;

[Collection("ControllerTests")]
public class ArtistControllerTests
{
    private readonly Mock<IArtistService> _mockArtistService;
    private readonly Mock<ITrackService> _mockTrackService;
    private readonly ArtistController _artistController;

    public ArtistControllerTests()
    {
        _mockArtistService = new Mock<IArtistService>();
        _mockTrackService = new Mock<ITrackService>();
        _artistController = new ArtistController(
            _mockArtistService.Object,
            _mockTrackService.Object
        );
    }

    /********** Getters **********/
    [Fact]
    public async Task Get_ReturnsOk()
    {
        // Arrange
        var artistDto = new ArtistDto();
        var artistDtoList = new List<ArtistDto> { artistDto };
        _mockArtistService.Setup(x => x.GetListAsync()).ReturnsAsync(artistDtoList);

        // Act
        var result = await _artistController.Get();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Get_ReturnsListOfArtists()
    {
        // Arrange
        var artistDto = new ArtistDto();
        var artistDtoList = new List<ArtistDto> { artistDto };
        _mockArtistService.Setup(x => x.GetListAsync()).ReturnsAsync(artistDtoList);

        // Act
        var result = await _artistController.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<List<ArtistDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsOk_IfIdIsValid()
    {
        // Arrange
        var artistDto = new ArtistDto();
        _mockArtistService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(artistDto);

        // Act
        var result = await _artistController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_ReturnsArtist_IfIdIsValid()
    {
        // Arrange
        var artistDto = new ArtistDto();
        _mockArtistService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(artistDto);

        // Act
        var result = await _artistController.GetById(It.IsAny<Guid>());

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult?.Value);
        Assert.IsType<ArtistDto>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_IfIdIsInvalid()
    {
        // Arrange
        _mockArtistService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ArtistDto?)null);

        // Act
        var result = await _artistController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetArtistsByTrackId_ReturnsOk_IfIdIsValid()
    {
        // Arrange
        var artistDto = new ArtistDto();
        var track = new TrackDto { Id = new Guid("b3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3") };
        var artistDtoList = new List<ArtistDto> { artistDto };

        _mockTrackService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(track);
        _mockArtistService
            .Setup(x => x.GetArtistsByTrackIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(artistDtoList);

        // Act
        var result = await _artistController.GetArtistsByTrackId(track.Id);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetArtistsByTrackId_ReturnsListOfArtists_IfIdIsValid()
    {
        // Arrange
        var artistDto = new ArtistDto();
        var track = new TrackDto { Id = new Guid("b3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3") };
        var artistDtoList = new List<ArtistDto> { artistDto };

        _mockTrackService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(track);
        _mockArtistService
            .Setup(x => x.GetArtistsByTrackIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(artistDtoList);

        // Act
        var result = await _artistController.GetArtistsByTrackId(track.Id);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<List<ArtistDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetArtistsByTrackId_ReturnsNotFound_IfIdIsInvalid()
    {
        // Arrange
        _mockArtistService
            .Setup(x => x.GetArtistsByTrackIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((List<ArtistDto>?)null);

        // Act
        var result = await _artistController.GetArtistsByTrackId(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetArtistsByTrackId_ReturnsNotFound_IfTrackIsNull()
    {
        // Arrange
        var track = new TrackDto();
        IReadOnlyList<ArtistDto> trackList = new List<ArtistDto>();
        track.Id = new Guid("b3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3");
        _mockTrackService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(track);
        _mockArtistService
            .Setup(x => x.GetArtistsByTrackIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(trackList);

        // Act
        var result = await _artistController.GetArtistsByTrackId(track.Id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    /********** Create **********/
    [Fact]
    public async Task Create_ReturnsOk_IfArtistIsValid()
    {
        // Arrange
        var artistDto = new ArtistDto();
        _mockArtistService.Setup(x => x.CreateAsync(It.IsAny<ArtistDto>())).ReturnsAsync(artistDto);

        // Act
        var result = await _artistController.Create(artistDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsArtist_IfArtistIsValid()
    {
        // Arrange
        var artistDto = new ArtistDto();
        _mockArtistService.Setup(x => x.CreateAsync(It.IsAny<ArtistDto>())).ReturnsAsync(artistDto);

        // Act
        var result = await _artistController.Create(artistDto);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<ArtistDto>(okResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsNotFound_IfArtistIsNull()
    {
        // Arrange
        var artistDto = new ArtistDto();
        _mockArtistService.Setup(x => x.CreateAsync(artistDto)).ReturnsAsync((ArtistDto?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _artistController.Create(It.IsAny<ArtistDto>())
        );
        Assert.Equal("Something went wrong with the Artist creation", exception.Message);
    }

    /********** Update **********/
    [Fact]
    public async Task Update_ReturnsOk_IfArtistIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var artistDto = new ArtistDto();
        var artistDtoUpdate = new ArtistDto();
        _mockArtistService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(artistDto);
        _mockArtistService.Setup(x => x.UpdateAsync(id, artistDtoUpdate)).ReturnsAsync(artistDto);

        // Act
        var result = await _artistController.Update(id, artistDtoUpdate);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsArtist_IfArtistIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var artistDto = new ArtistDto();
        var artistDtoUpdate = new ArtistDto();
        _mockArtistService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(artistDto);
        _mockArtistService.Setup(x => x.UpdateAsync(id, artistDtoUpdate)).ReturnsAsync(artistDto);

        // Act
        var result = await _artistController.Update(id, artistDtoUpdate);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<ArtistDto>(okResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_IfArtistIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        ArtistDto artistDtoUpdate = new ArtistDto();
        _mockArtistService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((ArtistDto?)null);
        _mockArtistService
            .Setup(x => x.UpdateAsync(id, artistDtoUpdate))
            .ReturnsAsync((ArtistDto?)null);

        // Act
        var result = await _artistController.Update(id, artistDtoUpdate);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Update_ThrowsException_IfUpdatedArtistIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        ArtistDto artistDto = new ArtistDto();
        ArtistDto artistDtoUpdate = new ArtistDto();
        _mockArtistService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(artistDto);
        _mockArtistService
            .Setup(x => x.UpdateAsync(id, artistDtoUpdate))
            .ReturnsAsync((ArtistDto?)null);

        // Act

        // Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _artistController.Update(id, artistDtoUpdate)
        );
        Assert.Equal("Something went wrong with the update of the artist", exception.Message);
    }

    /********** Delete **********/
    [Fact]
    public async Task Delete_ReturnsOk_IfArtistIsValid()
    {
        // Arrange
        _mockArtistService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new ArtistDto());
        _mockArtistService.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        // Act
        var result = await _artistController.Delete(It.IsAny<Guid>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_IfArtistIsNull()
    {
        // Arrange
        _mockArtistService.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        var result = await _artistController.Delete(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ThrowsException_IfArtistIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _mockArtistService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new ArtistDto());
        _mockArtistService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(false);

        // Act

        // Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _artistController.Delete(id)
        );
        Assert.Equal("Something went wrong with the deletion of the artist", exception.Message);
    }
}
