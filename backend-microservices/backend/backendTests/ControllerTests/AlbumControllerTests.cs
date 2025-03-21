// Copyright : Pierre FRAISSE
// backend>backendTests>AlbumControllerTests.cs
// Created : 2024/05/1414 - 13:05

using API.Controllers;
using Core.DTOs.AlbumDTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace backendTests.ControllerTests;

[Collection("ControllerTests")]
public class AlbumControllerTests
{
    private readonly Mock<IAlbumService> _mockAlbumService;
    private readonly Mock<IFileValidationService> _mockFileValidationService;
    private readonly Mock<UserManager<AppUser>> _mockUserManager;

    private readonly AlbumController _albumController;

    public AlbumControllerTests()
    {
        _mockAlbumService = new Mock<IAlbumService>();
        _mockFileValidationService = new Mock<IFileValidationService>();
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _mockUserManager = new Mock<UserManager<AppUser>>(
            userStoreMock.Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<AppUser>>().Object,
            new IUserValidator<AppUser>[] { },
            new IPasswordValidator<AppUser>[] { },
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<AppUser>>>().Object
        );
        _albumController = new AlbumController(
            _mockAlbumService.Object,
            _mockFileValidationService.Object,
            _mockUserManager.Object
        );
    }

    private readonly string _userId = "userId";

    /********** Getters **********/
    [Fact]
    public async Task Get_ReturnsOk()
    {
        // Arrange
        var albumDto = new AlbumDto();
        var albumDtoList = new List<AlbumDto> { albumDto };
        _mockAlbumService.Setup(x => x.GetListAsync()).ReturnsAsync(albumDtoList);

        // Act
        var result = await _albumController.Get();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Get_ReturnsListOfAlbums()
    {
        // Arrange
        var albumDto = new AlbumDto();
        var albumDtoList = new List<AlbumDto> { albumDto };
        _mockAlbumService.Setup(x => x.GetListAsync()).ReturnsAsync(albumDtoList);

        // Act
        var result = await _albumController.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<List<AlbumDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsOk_IfIdIsValid()
    {
        // Arrange
        var albumDto = new AlbumDto();
        _mockAlbumService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(albumDto);

        // Act
        var result = await _albumController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_ReturnsAlbum_IfIdIsValid()
    {
        // Arrange
        var albumDto = new AlbumDto();
        _mockAlbumService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(albumDto);

        // Act
        var result = await _albumController.GetById(It.IsAny<Guid>());

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<AlbumDto>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_IfIdIsInvalid()
    {
        // Arrange
        _mockAlbumService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((AlbumDto?)null);

        // Act
        var result = await _albumController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAlbumListByUserId_ShouldReturnAlbumList_IfUserIdIsValid()
    {
        // Arrange
        AlbumDto albumDto = new AlbumDto();
        List<AlbumDto> albumDtoList = [albumDto];

        _mockUserManager
            .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(new AppUser());
        _mockAlbumService
            .Setup(x => x.GetAlbumListByUserIdAsync(It.IsAny<string>()))
            .ReturnsAsync(albumDtoList);

        // Act
        var result = await _albumController.GetAlbumListByUserId(_userId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<List<AlbumDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetAlbumListByUserId_ShouldReturnNotFound_IfUserIdIsInvalid()
    {
        // Arrange

        // Act
        var result = await _albumController.GetAlbumListByUserId(_userId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetAlbumListByUserId_ShouldReturnNotFound_IfAlbumListIsEmpty()
    {
        // Arrange
        IReadOnlyList<AlbumDto> albumDtoList = new List<AlbumDto>();

        _mockUserManager
            .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(new AppUser());
        _mockAlbumService
            .Setup(x => x.GetAlbumListByUserIdAsync(It.IsAny<string>()))
            .ReturnsAsync(albumDtoList);

        // Act
        var result = await _albumController.GetAlbumListByUserId(_userId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    /********** Create **********/
    [Fact]
    public async Task Create_ReturnsOk_IfAlbumIsValid()
    {
        // Arrange
        AlbumDto albumDto = new AlbumDto();
        _mockAlbumService.Setup(x => x.CreateWithFileAsync(albumDto, null)).ReturnsAsync(albumDto);

        // Act
        var result = await _albumController.Create(albumDto, null);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsAlbum_IfAlbumIsValid()
    {
        // Arrange
        AlbumDto albumDto = new AlbumDto();
        _mockAlbumService.Setup(x => x.CreateWithFileAsync(albumDto, null)).ReturnsAsync(albumDto);

        // Act
        var result = await _albumController.Create(albumDto, null);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<AlbumDto>(okResult.Value);
    }

    [Fact]
    public async Task Create_ShouldCallValidatePictureFile_IfFileIsNotNull()
    {
        // Arrange
        AlbumDto albumDto = new AlbumDto();
        IFormFile file = new Mock<IFormFile>().Object;
        _mockAlbumService.Setup(x => x.CreateWithFileAsync(albumDto, file)).ReturnsAsync(albumDto);

        // Act
        await _albumController.Create(albumDto, file);

        // Assert
        _mockFileValidationService.Verify(x => x.ValidatePictureFile(file), Times.Once);
    }

    [Fact]
    public async Task Create_ReturnsException_IfAlbumIsInvalid()
    {
        // Arrange
        AlbumDto albumDto = new AlbumDto();
        _mockAlbumService
            .Setup(x => x.CreateWithFileAsync(albumDto, null))
            .ReturnsAsync((AlbumDto?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _albumController.Create(albumDto, null)
        );
        Assert.Equal("Something went wrong with the Album creation", exception.Message);
    }

    /********** Update **********/
    [Fact]
    public async Task Update_ReturnsOk_IfAlbumIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AlbumDto albumDto = new AlbumDto();
        AlbumDto albumUpdateDto = new AlbumDto();
        _mockAlbumService
            .Setup(x =>
                x.UpdateWithFileAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<AlbumDto>(),
                    It.IsAny<IFormFile>()
                )
            )
            .ReturnsAsync(albumDto);

        // Act
        var result = await _albumController.Update(id, albumUpdateDto, null);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsAlbum_IfAlbumIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AlbumDto albumDto = new AlbumDto();
        AlbumDto albumUpdateDto = new AlbumDto();
        _mockAlbumService
            .Setup(x =>
                x.UpdateWithFileAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<AlbumDto>(),
                    It.IsAny<IFormFile>()
                )
            )
            .ReturnsAsync(albumDto);

        // Act
        var result = await _albumController.Update(id, albumUpdateDto, null);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<AlbumDto>(okResult.Value);
    }

    [Fact]
    public async Task Update_ShouldCallValidatePictureFile_IfFileIsNotNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AlbumDto albumDto = new AlbumDto();
        AlbumDto albumUpdateDto = new AlbumDto();
        IFormFile file = new Mock<IFormFile>().Object;
        _mockAlbumService
            .Setup(x => x.UpdateWithFileAsync(id, albumUpdateDto, null))
            .ReturnsAsync(albumDto);

        // Act
        await _albumController.Update(id, albumUpdateDto, file);

        // Assert
        _mockFileValidationService.Verify(x => x.ValidatePictureFile(file), Times.Once);
    }

    /********** Delete **********/
    [Fact]
    public async Task Delete_ReturnsOk_IfAlbumIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _mockAlbumService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new AlbumDto());
        _mockAlbumService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(true);

        // Act
        var result = await _albumController.Delete(id);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_IfAlbumIsInvalid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _mockAlbumService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(It.IsAny<bool>());

        // Act
        var result = await _albumController.Delete(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnAnException_IfAlbumIsNotDeleted()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _mockAlbumService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new AlbumDto());
        _mockAlbumService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _albumController.Delete(id)
        );
        Assert.Equal("Something went wrong with the Album deletion", exception.Message);
    }
}
