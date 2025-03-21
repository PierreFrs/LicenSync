// Copyright : Pierre FRAISSE
// backend>backendTests>GenreControllerTests.cs
// Created : 2024/05/1414 - 13:05

using API.Controllers;
using Core.DTOs.GenreDTOs;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace backendTests.ControllerTests;

[Collection("ControllerTests")]
public class GenreControllerTests
{
    private readonly Mock<IGenreService> _mockGenreService;
    private readonly GenreController _genreController;

    public GenreControllerTests()
    {
        _mockGenreService = new Mock<IGenreService>();
        _genreController = new GenreController(_mockGenreService.Object);
    }

    /********** Getters **********/
    [Fact]
    public async Task Get_ReturnsOk()
    {
        // Arrange
        var genreDto = new GenreDto();
        var genreDtoList = new List<GenreDto> { genreDto };
        _mockGenreService.Setup(x => x.GetListAsync()).ReturnsAsync(genreDtoList);

        // Act
        var result = await _genreController.Get();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Get_ReturnsListOfGenres()
    {
        // Arrange
        var genreDto = new GenreDto();
        var genreDtoList = new List<GenreDto> { genreDto };
        _mockGenreService.Setup(x => x.GetListAsync()).ReturnsAsync(genreDtoList);

        // Act
        var result = await _genreController.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<List<GenreDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsOk_IfIdIsValid()
    {
        // Arrange
        var genreDto = new GenreDto();
        var genreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7");
        _mockGenreService.Setup(x => x.GetByIdAsync(genreId)).ReturnsAsync(genreDto);

        // Act
        var result = await _genreController.GetById(genreId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_ReturnsGenre_IfIdIsValid()
    {
        // Arrange
        var genreDto = new GenreDto();
        var genreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7");
        _mockGenreService.Setup(x => x.GetByIdAsync(genreId)).ReturnsAsync(genreDto);

        // Act
        var result = await _genreController.GetById(genreId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<GenreDto>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_IfIdIsInvalid()
    {
        // Arrange
        _mockGenreService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((GenreDto?)null);

        // Act
        var result = await _genreController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    /********** Create **********/
    [Fact]
    public async Task Create_ReturnsOk()
    {
        // Arrange
        var genreDto = new GenreDto();
        _mockGenreService.Setup(x => x.CreateAsync(genreDto)).ReturnsAsync(genreDto);

        // Act
        var result = await _genreController.Create(genreDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsGenre()
    {
        // Arrange
        var genreDto = new GenreDto();
        _mockGenreService.Setup(x => x.CreateAsync(genreDto)).ReturnsAsync(genreDto);

        // Act
        var result = await _genreController.Create(genreDto);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<GenreDto>(okResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsAnException_IfGenreIsNull()
    {
        // Arrange
        GenreDto genreDto = new GenreDto();
        _mockGenreService.Setup(x => x.CreateAsync(genreDto)).ReturnsAsync((GenreDto?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _genreController.Create(genreDto));
    }

    /********** Update **********/
    [Fact]
    public async Task Update_ReturnsOk_IfGenreIsValid()
    {
        // Arrange
        var genreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7");
        var genreDto = new GenreDto();
        var genreDtoUpdate = new GenreDto();
        _mockGenreService.Setup(x => x.UpdateAsync(genreId, genreDtoUpdate)).ReturnsAsync(genreDto);

        // Act
        var result = await _genreController.Update(genreId, genreDtoUpdate);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsGenre_IfGenreIsValid()
    {
        // Arrange
        var genreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7");
        var genreDto = new GenreDto();
        var genreDtoUpdate = new GenreDto();
        _mockGenreService.Setup(x => x.UpdateAsync(genreId, genreDtoUpdate)).ReturnsAsync(genreDto);

        // Act
        var result = await _genreController.Update(genreId, genreDtoUpdate);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<GenreDto>(okResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_IfGenreIsNull()
    {
        // Arrange
        var genreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7");
        var genreDtoUpdate = new GenreDto();
        _mockGenreService
            .Setup(x => x.UpdateAsync(genreId, genreDtoUpdate))
            .ReturnsAsync((GenreDto?)null);

        // Act
        var result = await _genreController.Update(genreId, genreDtoUpdate);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    /********** Delete **********/
    [Fact]
    public async Task Delete_ReturnsOk_IfIdIsValid()
    {
        // Arrange
        var genreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7");
        _mockGenreService.Setup(x => x.GetByIdAsync(genreId)).ReturnsAsync(new GenreDto());
        _mockGenreService.Setup(x => x.DeleteAsync(genreId)).ReturnsAsync(true);

        // Act
        var result = await _genreController.Delete(genreId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsException_IfDeleteReturnsFalse()
    {
        // Arrange
        Guid genreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7");
        _mockGenreService.Setup(x => x.GetByIdAsync(genreId)).ReturnsAsync(new GenreDto());
        _mockGenreService.Setup(x => x.DeleteAsync(genreId)).ReturnsAsync(false);

        // Act

        // Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _genreController.Delete(genreId)
        );
        Assert.Equal("Something went wrong with the genre deletion", exception.Message);
    }
}
