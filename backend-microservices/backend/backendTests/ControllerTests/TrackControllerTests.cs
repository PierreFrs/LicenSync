// Copyright : Pierre FRAISSE
// backend>backendTests>TrackControllerTests.cs
// Created : 2024/05/1414 - 13:05

using API.Controllers;
using Core.DTOs.TrackDTOs;
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
public class TrackControllerTests
{
    private readonly Mock<ITrackService> _mockTrackService;
    private readonly Mock<IFileValidationService> _mockFileValidationService;
    private readonly UserManager<AppUser> _userManager;
    private readonly TrackController _trackController;

    public TrackControllerTests()
    {
        _mockTrackService = new Mock<ITrackService>();
        _mockFileValidationService = new Mock<IFileValidationService>();
        
        // Setup UserManager mock with required dependencies
        _userManager = new UserManager<AppUser>(
            new Mock<IUserStore<AppUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<AppUser>>().Object,
            new IUserValidator<AppUser>[] { },
            new IPasswordValidator<AppUser>[] { },
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<AppUser>>>().Object
        );

        _trackController = new TrackController(
            _mockTrackService.Object,
            _mockFileValidationService.Object,
            _userManager
        );
    }

    /********** Getters **********/
    [Fact]
    public async Task Get_ReturnsOk()
    {
        // Arrange
        var trackDto = new TrackDto();
        var trackDtoList = new List<TrackDto> { trackDto };
        _mockTrackService.Setup(x => x.GetListAsync()).ReturnsAsync(trackDtoList);

        // Act
        var result = await _trackController.Get();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Get_ReturnsListOfTracks()
    {
        // Arrange
        var trackDto = new TrackDto();
        var trackDtoList = new List<TrackDto> { trackDto };
        _mockTrackService.Setup(x => x.GetListAsync()).ReturnsAsync(trackDtoList);

        // Act
        var result = await _trackController.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<List<TrackDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsOk()
    {
        // Arrange
        var trackDto = new TrackDto();
        _mockTrackService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(trackDto);

        // Act
        var result = await _trackController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_IfTrackIsNull()
    {
        // Arrange
        _mockTrackService
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((TrackDto?)null);

        // Act
        var result = await _trackController.GetById(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetByUserId_ReturnsOk_ifReturnsList()
    {
        // Arrange
        var trackDto = new TrackDto();
        var trackDtoList = new List<TrackDto> { trackDto };
        _mockTrackService
            .Setup(x => x.GetByUserIdAsync(It.IsAny<string>()))
            .ReturnsAsync(trackDtoList);

        // Act
        var result = await _trackController.GetByUserId(It.IsAny<string>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetByUserId_ReturnsListOfTracks()
    {
        // Arrange
        var trackDto = new TrackDto();
        var trackDtoList = new List<TrackDto> { trackDto };
        _mockTrackService
            .Setup(x => x.GetByUserIdAsync(It.IsAny<string>()))
            .ReturnsAsync(trackDtoList);

        // Act
        var result = await _trackController.GetByUserId(It.IsAny<string>());

        // Assert
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<List<TrackDto>>(okResult.Value);
    }

    [Fact]
    public async Task GetPictureByTrackId_ReturnsOk_IfTrackPictureExistsAndHaveAJpgExtension()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tempFileName = Path.GetTempFileName();
        var tempFileNameWithExtension = Path.ChangeExtension(tempFileName, ".jpg");
        var filename = Path.GetFileName(tempFileNameWithExtension);
        FileStream returnedFile = File.Create(tempFileNameWithExtension);

        try
        {
            var tuple = (returnedFile, filename);
            _mockTrackService.Setup(x => x.GetPictureByTrackIdAsync(id)).ReturnsAsync(tuple);

            // Act
            var result = await _trackController.GetPictureByTrackId(id);

            // Assert
            var fileResult = Assert.IsType<FileStreamResult>(result);
            Assert.Equal("image/jpeg", fileResult.ContentType);
            Assert.Equal(filename, fileResult.FileDownloadName);
        }
        finally
        {
            await returnedFile.DisposeAsync();
            File.Delete(tempFileName);
        }
    }

    [Fact]
    public async Task GetPictureByTrackId_ReturnsOk_IfTrackPictureExistsAndHaveAPngExtension()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tempFileName = Path.GetTempFileName();
        var tempFileNameWithExtension = Path.ChangeExtension(tempFileName, ".png");
        var filename = Path.GetFileName(tempFileNameWithExtension);
        FileStream returnedFile = File.Create(tempFileNameWithExtension);

        try
        {
            var tuple = (returnedFile, filename);
            _mockTrackService.Setup(x => x.GetPictureByTrackIdAsync(id)).ReturnsAsync(tuple);

            // Act
            var result = await _trackController.GetPictureByTrackId(id);

            // Assert
            var fileResult = Assert.IsType<FileStreamResult>(result);
            Assert.Equal("image/png", fileResult.ContentType);
            Assert.Equal(filename, fileResult.FileDownloadName);
        }
        finally
        {
            await returnedFile.DisposeAsync();
            File.Delete(tempFileName);
        }
    }

    [Fact]
    public async Task GetPictureByTrackId_ShouldReturnNotFound_IfFileStreamIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _mockTrackService
            .Setup(service => service.GetPictureByTrackIdAsync(id))!
            .ReturnsAsync(new ValueTuple<FileStream?, string>(null, "filepath"));

        // Act
        var result = await _trackController.GetPictureByTrackId(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    /********** Create **********/
    [Fact]
    public async Task Create_ReturnsOk()
    {
        // Arrange
        var trackDto = new TrackDto();
        var audioFile = new Mock<IFormFile>();
        var visualFile = new Mock<IFormFile>();
        _mockTrackService
            .Setup(x =>
                x.CreateWithFilesAsync(
                    It.IsAny<TrackDto>(),
                    It.IsAny<IFormFile>(),
                    It.IsAny<IFormFile>()
                )
            )
            .ReturnsAsync(trackDto);

        // Act
        var result = await _trackController.Create(trackDto, audioFile.Object, visualFile.Object);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsException_IfTrackIsNull()
    {
        // Arrange
        TrackDto trackDto = new TrackDto();
        var audioFile = new Mock<IFormFile>();
        var visualFile = new Mock<IFormFile>();
        _mockTrackService
            .Setup(x =>
                x.CreateWithFilesAsync(
                    It.IsAny<TrackDto>(),
                    It.IsAny<IFormFile>(),
                    It.IsAny<IFormFile>()
                )
            )
            .ReturnsAsync((TrackDto?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _trackController.Create(trackDto, audioFile.Object, visualFile.Object)
        );
        Assert.Equal("Something went wrong with the Track creation", exception.Message);
    }

    [Fact]
    public async Task Create_ThrowsArgumentException_IfAudioFileIsNull()
    {
        // Arrange
        var trackDto = new TrackDto();
        var visualFile = new Mock<IFormFile>();

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _trackController.Create(trackDto, null!, visualFile.Object)
        );

        // Assert
        Assert.Equal("Audio file is required and cannot be null.", exception.Message);
    }

    [Fact]
    public async Task Create_ThrowsArgumentException_IfAudioFileIsNotValid()
    {
        // Arrange
        var trackDto = new TrackDto();
        var audioFile = new Mock<IFormFile>();
        _mockFileValidationService
            .Setup(x => x.ValidateAudioFile(It.IsAny<IFormFile>()))
            .Throws(new ArgumentException("Audio file is not valid."));

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _trackController.Create(trackDto, audioFile.Object, null)
        );

        // Assert
        Assert.Equal("Audio file is not valid.", exception.Message);
    }

    [Fact]
    public async Task Create_ThrowsArgumentException_IfVisualFileIsNotValid()
    {
        // Arrange
        var trackDto = new TrackDto();
        var audioFile = new Mock<IFormFile>();
        var visualFile = new Mock<IFormFile>();
        _mockFileValidationService
            .Setup(x => x.ValidatePictureFile(It.IsAny<IFormFile>()))
            .Throws(new ArgumentException("Visual file is not valid."));

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _trackController.Create(trackDto, audioFile.Object, visualFile.Object)
        );

        // Assert
        Assert.Equal("Visual file is not valid.", exception.Message);
    }

    /********** Delete **********/
    [Fact]
    public async Task Delete_ReturnsOk()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        TrackDto trackDto = new TrackDto();
        _mockTrackService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(trackDto);
        _mockTrackService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(true);

        // Act
        var result = await _trackController.Delete(id);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_IfTrackIsNull()
    {
        // Arrange
        _mockTrackService.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        var result = await _trackController.Delete(It.IsAny<Guid>());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsException_IfTrackIsNotDeleted()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _mockTrackService.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new TrackDto());
        _mockTrackService.Setup(x => x.DeleteAsync(id)).ReturnsAsync(false);

        // Act

        // Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _trackController.Delete(id)
        );
        Assert.Equal("Something went wrong with the deletion of the track", exception.Message);
    }
}
