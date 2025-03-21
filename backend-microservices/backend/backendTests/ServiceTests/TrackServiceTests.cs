// Copyright : Pierre FRAISSE
// backend>backendTests>TrackServiceTests.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.TrackDTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Specifications;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace backendTests.ServiceTests;

[Collection("ServiceTests")]
public class TrackServiceTests
{
    private readonly Mock<ITrackRepository> _mockTrackRepository;
    private readonly Mock<IContributionService> _mockContributionService;
    private readonly Mock<IArtistService> _mockArtistService;
    private readonly Mock<IAlbumService> _mockAlbumService;
    private readonly Mock<IGenreService> _mockGenreService;
    private readonly Mock<IFileValidationService> _mockFileValidationService;
    private readonly IMapper _mapper;
    private readonly Mock<IFileHelpers> _mockFileHelpers;
    private readonly TrackService _trackService;

    public TrackServiceTests()
    {
        _mockTrackRepository = new Mock<ITrackRepository>();
        _mockContributionService = new Mock<IContributionService>();
        _mockArtistService = new Mock<IArtistService>();
        _mockAlbumService = new Mock<IAlbumService>();
        _mockGenreService = new Mock<IGenreService>();
        _mockFileValidationService = new Mock<IFileValidationService>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Track, TrackDto>().ReverseMap();
        });
        _mapper = mapperConfig.CreateMapper();

        _mockFileHelpers = new Mock<IFileHelpers>();

        var fileStorageSettings = new FileStorageSettings
        {
            TrackAudioFilesFolder = "testAudioFolder",
            TrackVisualFilesFolder = "testVisualFolder",
        };
        Mock<IOptions<FileStorageSettings>> mockFileStorageSettings = new();
        mockFileStorageSettings.Setup(x => x.Value).Returns(fileStorageSettings);

        _trackService = new TrackService(
            _mockTrackRepository.Object,
            _mockContributionService.Object,
            _mockArtistService.Object,
            _mockAlbumService.Object,
            _mockGenreService.Object,
            _mapper,
            mockFileStorageSettings.Object,
            _mockFileHelpers.Object,
            _mockFileValidationService.Object
        );
    }

    private readonly string _userId = "userId";

    /********** Getters **********/
    [Fact]
    public async Task GetListAsync_ShouldReturnAListOfTrackDtos_WhenDetailsAreValid()
    {
        // Arrange
        var mockTracks = new List<Track>
        {
            new Track
            {
                Id = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"),
                TrackTitle = "white_room.mp3",
                RecordLabel = "Polydor",
                Length = "03:04:00",
                AudioFilePath = "/test/file/path/white_room.mp3",
                TrackVisualPath = "/test/file/path/white_room_visual.jpg",
                FirstGenreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
                SecondaryGenreId = new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
                AlbumId = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            },
            new Track
            {
                Id = new Guid("20fd521b-f136-4036-83c7-c384769d8e69"),
                TrackTitle = "cream.mp3",
                RecordLabel = "Loud",
                Length = "04:12:00",
                AudioFilePath = "/test/file/path/cream.mp3",
                TrackVisualPath = "/test/file/path/cream_visual.jpg",
                FirstGenreId = new Guid("028450d1-783e-45c6-a59f-0241ced8731c"),
                SecondaryGenreId = new Guid("e79ff304-68df-46f1-861d-7a969c479fa1"),
                AlbumId = new Guid("d2a246c9-6af5-4710-898d-4dfd0a772153"),
                UserId = "3ff1781d-6979-4760-8401-8ab29522f9af",
            },
        };

        _mockTrackRepository.Setup((repo => repo.GetListAsync())).ReturnsAsync(mockTracks);

        var mockTrackDtos = _mapper.Map<List<TrackDto>>(mockTracks);

        // Act
        var result = await _trackService.GetListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<TrackDto>>(result);
        Assert.Equal(mockTrackDtos.Count, result.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnATrackDto_WhenIdIsValid()
    {
        // Arrange
        var testTrack = new Track
        {
            Id = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"),
            TrackTitle = "white_room.mp3",
            RecordLabel = "Polydor",
            Length = "03:04:00",
            AudioFilePath = "/test/file/path/white_room.mp3",
            TrackVisualPath = "/test/file/path/white_room_visual.jpg",
            FirstGenreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
            SecondaryGenreId = new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
            AlbumId = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockTrackRepository
            .Setup(repo => repo.GetByIdAsync(new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327")))
            .ReturnsAsync(testTrack);

        // Act
        var expectedTrackDto = await _trackService.GetByIdAsync(
            new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327")
        );

        // Assert
        Assert.NotNull(expectedTrackDto);
        Assert.IsType<TrackDto>(expectedTrackDto);
        Assert.Equal(new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"), expectedTrackDto.Id);
        Assert.Equal("white_room.mp3", expectedTrackDto.TrackTitle);
        Assert.Equal("Polydor", expectedTrackDto.RecordLabel);
        Assert.Equal("03:04:00", expectedTrackDto.Length);
        Assert.Equal("/test/file/path/white_room.mp3", expectedTrackDto.AudioFilePath);
        Assert.Equal("/test/file/path/white_room_visual.jpg", expectedTrackDto.TrackVisualPath);
        Assert.Equal(
            new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
            expectedTrackDto.FirstGenreId
        );
        Assert.Equal(
            new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
            expectedTrackDto.SecondaryGenreId
        );
        Assert.Equal(new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"), expectedTrackDto.AlbumId);
        Assert.Equal("46b0f619-f02d-4d31-823b-465cfb01cae4", expectedTrackDto.UserId);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenIdIsInvalid()
    {
        // Arrange
        _mockTrackRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(It.IsAny<Track>());

        // Act
        var expectedTrackDto = await _trackService.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(expectedTrackDto);
    }

    /********** GetByUserId **********/

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnAListOfTrackDtos_WhenUserIdIsValid()
    {
        // Arrange
        var testTracks = new List<Track>
        {
            new Track
            {
                Id = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"),
                TrackTitle = "white_room.mp3",
                RecordLabel = "Polydor",
                Length = "03:04:00",
                AudioFilePath = "/test/file/path/white_room.mp3",
                TrackVisualPath = "/test/file/path/white_room_visual.jpg",
                FirstGenreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
                SecondaryGenreId = new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
                AlbumId = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            },
            new Track
            {
                Id = new Guid("20fd521b-f136-4036-83c7-c384769d8e69"),
                TrackTitle = "cream.mp3",
                RecordLabel = "Loud",
                Length = "04:12:00",
                AudioFilePath = "/test/file/path/cream.mp3",
                TrackVisualPath = "/test/file/path/cream_visual.jpg",
                FirstGenreId = new Guid("028450d1-783e-45c6-a59f-0241ced8731c"),
                SecondaryGenreId = new Guid("e79ff304-68df-46f1-861d-7a969c479fa1"),
                AlbumId = new Guid("d2a246c9-6af5-4710-898d-4dfd0a772153"),
                UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            },
        };

        _mockTrackRepository
            .Setup(repo => repo.GetEntityListBySpecificationAsync(It.IsAny<TrackSpecification>()))
            .ReturnsAsync(testTracks);

        // Act
        var expectedTrackDtos = await _trackService.GetByUserIdAsync(
            "46b0f619-f02d-4d31-823b-465cfb01cae4"
        );

        // Assert
        Assert.NotNull(expectedTrackDtos);
        Assert.IsType<List<TrackDto>>(expectedTrackDtos);
        Assert.Equal(testTracks.Count, expectedTrackDtos.Count);
    }

    /********** GetPictureByTrackId **********/

    [Fact]
    public async Task GetPictureByTrackIdAsync_ShouldReturnFileStreamAndFilename_WhenIdIsValid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedFileName = "testImage.jpg";
        var testTrack = new Track
        {
            Id = id,
            TrackTitle = "white_room",
            RecordLabel = "Polydor",
            Length = "03:04:00",
            AudioFilePath = "/test/file/path/white_room.mp3",
            TrackVisualPath = $"/test/file/path/{expectedFileName}",
            FirstGenreId = Guid.NewGuid(),
            SecondaryGenreId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
            UserId = _userId,
        };

        string tempFilePath = Path.Combine(Path.GetTempPath(), expectedFileName);

        testTrack.TrackVisualPath = tempFilePath;

        _mockTrackRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(testTrack);

        FileStream? fileStream = null;

        try
        {
            // Ensure the file is created in the temporary path
            await File.WriteAllTextAsync(tempFilePath, "dummy image content");

            // Act
            (fileStream, var filename) = await _trackService.GetPictureByTrackIdAsync(id);

            // Assert
            Assert.NotNull(fileStream);
            Assert.IsType<FileStream>(fileStream);
            Assert.Equal(expectedFileName, filename);

            // Additional checks can be made here if necessary, e.g., fileStream content, length, etc.
        }
        finally
        {
            if (fileStream != null)
            {
                await fileStream.DisposeAsync();
            }

            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }

    [Fact]
    public async Task GetPictureByTrackIdAsync_ShouldReturnNullValues_WhenTrackIsNotFound()
    {
        // Arrange
        _mockTrackRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Track?)null); // Simulate no track found

        // Act
        var result = await _trackService.GetPictureByTrackIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result.Item1); // FileStream should be null
        Assert.Null(result.Item2); // Filename should be null
    }

    /********** Create **********/

    [Fact]
    public async Task CreateAsync_ShouldCallGetAudioFileLengthAsync_ifAudioFileIsNotNull()
    {
        // Arrange
        var mockAudioFile = new Mock<IFormFile>();
        var trackDto = new TrackDto
        {
            Id = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"),
            TrackTitle = "white_room.mp3",
            RecordLabel = "Polydor",
            Length = "03:04:00",
            AudioFilePath = "/test/file/path/white_room.mp3",
            TrackVisualPath = "/test/file/path/white_room_visual.jpg",
            FirstGenreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
            SecondaryGenreId = new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
            AlbumId = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockFileHelpers
            .Setup(f => f.GetAudioFileLengthAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync("03:04:00");

        // Act
        await _trackService.CreateWithFilesAsync(trackDto, mockAudioFile.Object, null);

        // Assert
        _mockFileHelpers.Verify(f => f.GetAudioFileLengthAsync(It.IsAny<IFormFile>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldCallSaveFileAsync_ifAudioFileIsNotNull()
    {
        // Arrange
        var mockAudioFile = new Mock<IFormFile>();
        var trackDto = new TrackDto();
        _mockFileHelpers
            .Setup(f => f.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
            .ReturnsAsync("path/to/audio");

        // Act
        await _trackService.CreateWithFilesAsync(trackDto, mockAudioFile.Object, null);

        // Assert
        _mockFileHelpers.Verify(
            f => f.SaveFileAsync(mockAudioFile.Object, It.IsAny<string>()),
            Times.Once
        );
    }

    [Fact]
    public async Task CreateAsync_ShouldCallSaveFileAsync_ifVisualFileIsNotNull()
    {
        // Arrange
        var audioFile = new Mock<IFormFile>();
        var visualFile = new Mock<IFormFile>();
        var trackDto = new TrackDto();
        _mockFileHelpers
            .Setup(f => f.SaveFileAsync(audioFile.Object, It.IsAny<string>()))
            .ReturnsAsync("path/to/visual");

        // Act
        await _trackService.CreateWithFilesAsync(trackDto, audioFile.Object, visualFile.Object);

        // Assert
        _mockFileHelpers.Verify(
            f => f.SaveFileAsync(visualFile.Object, It.IsAny<string>()),
            Times.Once
        );
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnTrackDto_IfDetailsAreValid()
    {
        // Arrange
        var mockAudioFile = new Mock<IFormFile>();
        var trackDto = new TrackDto
        {
            Id = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"),
            TrackTitle = "white_room.mp3",
            RecordLabel = "Polydor",
            Length = "03:04:00",
            AudioFilePath = "/test/file/path/white_room.mp3",
            TrackVisualPath = "/test/file/path/white_room_visual.jpg",
            FirstGenreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
            SecondaryGenreId = new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
            AlbumId = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        var track = new Track
        {
            Id = new Guid("ad7c9d85-1ef1-459f-8d34-91076009b327"),
            TrackTitle = "white_room.mp3",
            RecordLabel = "Polydor",
            Length = "03:04:00",
            AudioFilePath = "/test/file/path/white_room.mp3",
            TrackVisualPath = "/test/file/path/white_room_visual.jpg",
            FirstGenreId = new Guid("c1970414-805f-4f7a-9fdf-7b2de60a38f7"),
            SecondaryGenreId = new Guid("e4689f70-397c-4339-97c6-2fb4e129a155"),
            AlbumId = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockTrackRepository.Setup(repo => repo.CreateAsync(It.IsAny<Track>())).ReturnsAsync(track);

        // Act
        var expectedTrackDto = await _trackService.CreateWithFilesAsync(
            trackDto,
            mockAudioFile.Object,
            null
        );

        // Assert
        Assert.NotNull(expectedTrackDto);
        Assert.IsType<TrackDto>(expectedTrackDto);
    }

    [Fact]
    public async Task CreateWithFilesAsync_ShouldSetVisualFilePath_IfVisualFileIsNotNull()
    {
        // Arrange
        var mockAudioFile = new Mock<IFormFile>();
        var mockVisualFile = new Mock<IFormFile>();
        var trackDto = new TrackDto
        {
            Id = Guid.NewGuid(),
            TrackTitle = "new_track.mp3",
            RecordLabel = "TestLabel",
            FirstGenreId = Guid.NewGuid(),
            SecondaryGenreId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
            UserId = _userId,
        };

        var track = new Track
        {
            Id = trackDto.Id,
            TrackTitle = trackDto.TrackTitle,
            RecordLabel = trackDto.RecordLabel,
            FirstGenreId = trackDto.FirstGenreId,
            SecondaryGenreId = trackDto.SecondaryGenreId,
            AlbumId = trackDto.AlbumId,
            UserId = trackDto.UserId,
            AudioFilePath = "path/to/audio",
            TrackVisualPath = "path/to/visual",
            Length = "03:04:00",
        };

        _mockFileHelpers
            .Setup(f => f.GetAudioFileLengthAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync("03:04:00");
        _mockFileHelpers
            .Setup(f => f.SaveFileAsync(mockAudioFile.Object, It.IsAny<string>()))
            .ReturnsAsync("path/to/audio");
        _mockFileHelpers
            .Setup(f => f.SaveFileAsync(mockVisualFile.Object, It.IsAny<string>()))
            .ReturnsAsync("path/to/visual");

        _mockTrackRepository.Setup(repo => repo.CreateAsync(It.IsAny<Track>())).ReturnsAsync(track);

        // Act
        var result = await _trackService.CreateWithFilesAsync(
            trackDto,
            mockAudioFile.Object,
            mockVisualFile.Object
        );

        // Assert
        _mockFileHelpers.Verify(
            f => f.SaveFileAsync(mockVisualFile.Object, It.IsAny<string>()),
            Times.Once
        );
        Assert.NotNull(result);
        Assert.Equal("path/to/visual", result.TrackVisualPath);
    }

    /********** Update **********/

    [Fact]
    public async Task UpdateWithFilesAsync_ShouldSetVisualFilePath_IfVisualFileIsNotNull()
    {
        // Arrange
        var mockVisualFile = new Mock<IFormFile>();
        var trackDtoUpdate = new TrackDto
        {
            TrackTitle = "updated_track.mp3",
            RecordLabel = "UpdatedLabel",
            FirstGenreId = Guid.NewGuid(),
            SecondaryGenreId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
        };

        var existingTrack = new Track
        {
            Id = Guid.NewGuid(),
            TrackTitle = "old_track.mp3",
            RecordLabel = "OldLabel",
            FirstGenreId = Guid.NewGuid(),
            SecondaryGenreId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
            UserId = _userId,
            AudioFilePath = "path/to/audio",
            TrackVisualPath = "path/to/old_visual",
            Length = "03:04:00",
        };

        var updatedTrack = new Track
        {
            Id = existingTrack.Id,
            TrackTitle = trackDtoUpdate.TrackTitle,
            RecordLabel = trackDtoUpdate.RecordLabel,
            FirstGenreId = trackDtoUpdate.FirstGenreId,
            SecondaryGenreId = trackDtoUpdate.SecondaryGenreId,
            AlbumId = trackDtoUpdate.AlbumId,
            UserId = existingTrack.UserId,
            AudioFilePath = existingTrack.AudioFilePath,
            TrackVisualPath = "path/to/new_visual",
            Length = existingTrack.Length,
            UpdateDate = DateTime.Now,
        };

        _mockTrackRepository
            .Setup(repo => repo.GetByIdAsync(existingTrack.Id))
            .ReturnsAsync(existingTrack);
        _mockTrackRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Track>()))
            .ReturnsAsync(updatedTrack);
        _mockFileHelpers
            .Setup(f => f.SaveFileAsync(mockVisualFile.Object, It.IsAny<string>()))
            .ReturnsAsync("path/to/new_visual");
        _mockFileHelpers.Setup(f => f.DeleteFile(It.IsAny<string>())).Verifiable();

        // Act
        var result = await _trackService.UpdateWithFilesAsync(
            existingTrack.Id,
            trackDtoUpdate,
            mockVisualFile.Object
        );

        // Assert
        _mockFileHelpers.Verify(f => f.DeleteFile("path/to/old_visual"), Times.Once);
        _mockFileHelpers.Verify(
            f => f.SaveFileAsync(mockVisualFile.Object, It.IsAny<string>()),
            Times.Once
        );
        Assert.NotNull(result);
        Assert.Equal("path/to/new_visual", result.TrackVisualPath);
    }

    [Fact]
    public async Task UpdateWithFilesAsync_ShouldThrowAnException_IfTrackIsNull()
    {
        // Arrange
        _mockTrackRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(It.IsAny<Track>());

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _trackService.UpdateWithFilesAsync(Guid.NewGuid(), new TrackDto(), null)
        );
    }

    [Fact]
    public async Task UpdateWithFilesAsync_ShouldReturnNull_IfUpdateFails()
    {
        // Arrange
        var trackDtoUpdate = new TrackDto
        {
            TrackTitle = "updated_track.mp3",
            RecordLabel = "UpdatedLabel",
            FirstGenreId = Guid.NewGuid(),
            SecondaryGenreId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
        };

        var existingTrack = new Track
        {
            Id = Guid.NewGuid(),
            TrackTitle = "old_track.mp3",
            RecordLabel = "OldLabel",
            FirstGenreId = Guid.NewGuid(),
            SecondaryGenreId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
            UserId = _userId,
            AudioFilePath = "path/to/audio",
            TrackVisualPath = "path/to/old_visual",
            Length = "03:04:00",
        };

        _mockTrackRepository
            .Setup(repo => repo.GetByIdAsync(existingTrack.Id))
            .ReturnsAsync(existingTrack);
        _mockTrackRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Track>()))
            .ReturnsAsync((Track?)null);

        // Act
        var result = await _trackService.UpdateWithFilesAsync(
            existingTrack.Id,
            trackDtoUpdate,
            null
        );

        // Assert
        Assert.Null(result);
    }

    /********** Delete **********/
    [Fact]
    public async Task DeleteAsync_ShouldThrowException_IfTrackIsNull()
    {
        // Arrange
        _mockTrackRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(It.IsAny<Track>());

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _trackService.DeleteWithFilesAsync(Guid.NewGuid())
        );
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallDeleteFile_IfTrackVisualPathIsNotNull()
    {
        // Arrange
        var track = new Track
        {
            Id = Guid.NewGuid(),
            AudioFilePath = "path/to/audio/file",
            TrackVisualPath = "path/to/visual/file",
        };

        _mockTrackRepository.Setup(repo => repo.GetByIdAsync(track.Id)).ReturnsAsync(track);
        _mockTrackRepository.Setup(repo => repo.DeleteAsync(track.Id)).ReturnsAsync(true);

        // Act
        await _trackService.DeleteWithFilesAsync(track.Id);

        // Assert
        _mockFileHelpers.Verify(f => f.DeleteFile(track.TrackVisualPath), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallDeleteFile_IfAudioFilePathIsNotNull()
    {
        // Arrange
        var track = new Track
        {
            Id = Guid.NewGuid(),
            AudioFilePath = "path/to/audio/file",
            TrackVisualPath = "path/to/visual/file",
        };

        _mockTrackRepository.Setup(repo => repo.GetByIdAsync(track.Id)).ReturnsAsync(track);
        _mockTrackRepository.Setup(repo => repo.DeleteAsync(track.Id)).ReturnsAsync(true);

        // Act
        await _trackService.DeleteWithFilesAsync(track.Id);

        // Assert
        _mockFileHelpers.Verify(f => f.DeleteFile(track.AudioFilePath), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfDeleteIsSuccessful()
    {
        // Arrange
        var track = new Track { Id = Guid.NewGuid(), AudioFilePath = "path/to/audio/file" };
        _mockTrackRepository.Setup(repo => repo.GetByIdAsync(track.Id)).ReturnsAsync(track);
        _mockTrackRepository.Setup(repo => repo.DeleteAsync(track.Id)).ReturnsAsync(true);

        // Act
        var result = await _trackService.DeleteWithFilesAsync(track.Id);

        // Assert
        Assert.True(result);
    }
}
