// Copyright : Pierre FRAISSE
// backend>backendTests>AlbumServiceTests.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.AlbumDTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IRepositories;
using Core.Specifications;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;

namespace backendTests.ServiceTests;

[Collection("ServiceTests")]
public class AlbumServiceTests
{
    private readonly Mock<IAlbumRepository> _mockAlbumRepository;
    private readonly AlbumService _albumService;
    private readonly Mock<IFileHelpers> _mockFileHelpers;

    public AlbumServiceTests()
    {
        _mockAlbumRepository = new Mock<IAlbumRepository>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Album, AlbumDto>().ReverseMap();
        });
        var mapper = mapperConfig.CreateMapper();
        Mock<IOptions<FileStorageSettings>> mockFileStorageSettings = new();
        var fileStorageSettings = new FileStorageSettings { AlbumVisualsFolder = "testFolder" };
        mockFileStorageSettings.Setup(x => x.Value).Returns(fileStorageSettings);

        _mockFileHelpers = new Mock<IFileHelpers>();
        _albumService = new AlbumService(
            _mockAlbumRepository.Object,
            mapper,
            _mockFileHelpers.Object,
            mockFileStorageSettings.Object
        );
    }

    /********** Getters **********/
    [Fact]
    public async Task GetListAsync_ShouldReturnAlbumList()
    {
        // Arrange
        var albumList = new List<Album>
        {
            new Album
            {
                Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
                AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
                AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
                UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            },
            new Album
            {
                Id = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                AlbumTitle = "disraeli_gears",
                AlbumVisualPath = "/test/file/path/disraeli_gears",
                UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            },
        };
        _mockAlbumRepository.Setup(x => x.GetListAsync()).ReturnsAsync(albumList);

        // Act
        var expectedList = await _albumService.GetListAsync();

        // Assert
        Assert.Equal(albumList.Count, expectedList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAlbum_IfIdIsValid()
    {
        // Arrange
        var album = new Album
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        _mockAlbumRepository.Setup(x => x.GetByIdAsync(album.Id)).ReturnsAsync(album);

        // Act
        var result = await _albumService.GetByIdAsync(album.Id);

        // Assert
        Assert.Equal(album.Id, result?.Id);
        Assert.Equal(album.AlbumTitle, result?.AlbumTitle);
        Assert.Equal(album.AlbumVisualPath, result?.AlbumVisualPath);
        Assert.Equal(album.UserId, result?.UserId);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_IfIdIsInvalid()
    {
        // Arrange
        var albumId = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79");
        _mockAlbumRepository.Setup(x => x.GetByIdAsync(albumId)).ReturnsAsync((Album?)null);

        // Act
        var result = await _albumService.GetByIdAsync(albumId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAlbumListByUserIdAsync_ShouldReturnAlbumList_IfUserIdIsValid()
    {
        // Arrange
        var albumList = new List<Album>
        {
            new Album
            {
                Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
                AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
                AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
                UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            },
            new Album
            {
                Id = new Guid("42ddc682-eaa6-4ae4-bca6-c9672e1dfa14"),
                AlbumTitle = "disraeli_gears",
                AlbumVisualPath = "/test/file/path/disraeli_gears",
                UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            },
        };

        _mockAlbumRepository
            .Setup(repo => repo.GetEntityListBySpecificationAsync(It.IsAny<AlbumSpecification>()))
            .ReturnsAsync(albumList);

        // Act
        var result = await _albumService.GetAlbumListByUserIdAsync(albumList[0].UserId);

        // Assert
        if (result != null)
            Assert.Equal(albumList.Count, result.Count);
    }

    [Fact]
    public async Task GetAlbumListByUserIdAsync_ShouldReturnEmptyArray_IfUsersGotNoAlbum()
    {
        // Arrange
        var userId = "db06174b-2f0b-4fef-b41f-8550039b6a79";

        _mockAlbumRepository
            .Setup(repo => repo.GetEntityListBySpecificationAsync(It.IsAny<AlbumSpecification>()))
            .ReturnsAsync(new List<Album>());

        // Act
        var result = await _albumService.GetAlbumListByUserIdAsync(userId);

        // Assert
        Assert.Equal(Array.Empty<AlbumDto>(), result);
    }

    /********** Create **********/
    [Fact]
    public async Task CreateWithFileAsync_ShouldReturnCreatedAlbum_IfAlbumIsValid()
    {
        // Arrange
        var albumDto = new AlbumDto
        {
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        var album = new Album
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        _mockAlbumRepository.Setup(repo => repo.CreateAsync(It.IsAny<Album>())).ReturnsAsync(album);

        // Act
        var result = await _albumService.CreateWithFileAsync(albumDto, null);

        // Assert
        if (result != null)
        {
            Assert.Equal(album.Id, result.Id);
            Assert.Equal(album.AlbumTitle, result.AlbumTitle);
            Assert.Equal(album.AlbumVisualPath, result.AlbumVisualPath);
            Assert.Equal(album.UserId, result.UserId);
        }
    }

    [Fact]
    public async Task CreateWithFileAsync_ShouldReturnCreatedAlbum_IfAlbumIsValid_AndFileIsNotNull()
    {
        // Arrange
        var albumDto = new AlbumDto
        {
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        var album = new Album
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        _mockAlbumRepository.Setup(repo => repo.CreateAsync(It.IsAny<Album>())).ReturnsAsync(album);

        // Act
        var expectedAlbum = await _albumService.CreateWithFileAsync(
            albumDto,
            new Mock<IFormFile>().Object
        );

        // Assert
        if (expectedAlbum != null)
        {
            Assert.Equal(album.Id, expectedAlbum.Id);
            Assert.Equal(album.AlbumTitle, expectedAlbum.AlbumTitle);
            Assert.Equal(album.AlbumVisualPath, expectedAlbum.AlbumVisualPath);
            Assert.Equal(album.UserId, expectedAlbum.UserId);
        }
    }

    [Fact]
    public async Task CreateWithFileAsync_ShouldCallSaveFileAsync_IfAlbumVisualIsNotNull()
    {
        // Arrange

        var albumDto = new AlbumDto
        {
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        var album = new Album
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        _mockAlbumRepository.Setup(repo => repo.CreateAsync(It.IsAny<Album>())).ReturnsAsync(album);

        // Act
        await _albumService.CreateWithFileAsync(albumDto, new Mock<IFormFile>().Object);

        // Assert
        _mockFileHelpers.Verify(
            repo => repo.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()),
            Times.Once
        );
    }

    [Fact]
    public async Task CreateWithFileAsync_ShouldNotCallSaveFileAsync_IfAlbumVisualIsNull()
    {
        // Arrange

        var albumDto = new AlbumDto
        {
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        var album = new Album
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        _mockAlbumRepository.Setup(repo => repo.CreateAsync(It.IsAny<Album>())).ReturnsAsync(album);

        // Act
        await _albumService.CreateWithFileAsync(albumDto, null);

        // Assert
        _mockFileHelpers.Verify(
            fh => fh.SaveFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()),
            Times.Never
        );
    }

    /********** Update **********/
    [Fact]
    public async Task UpdateWithFileAsync_ShouldReturnUpdatedAlbum_IfAlbumIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        var newAlbum = new Album
        {
            Id = id,
            AlbumTitle = "honky_dory",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        var album = new Album
        {
            Id = id,
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        AlbumDto albumDtoUpdate = new AlbumDto()
        {
            AlbumTitle = "honky_dory",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockAlbumRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(album);
        _mockAlbumRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Album>()))
            .ReturnsAsync(newAlbum);

        // Act
        var expectedAlbum = await _albumService.UpdateWithFileAsync(id, albumDtoUpdate, null);

        // Assert
        Assert.Equal(id, expectedAlbum?.Id);
        Assert.Equal(albumDtoUpdate.AlbumTitle, expectedAlbum?.AlbumTitle);
        Assert.Equal(albumDtoUpdate.AlbumVisualPath, expectedAlbum?.AlbumVisualPath);
        Assert.Equal(albumDtoUpdate.UserId, expectedAlbum?.UserId);
    }

    [Fact]
    public async Task UpdateWithFileAsync_ShouldReturnUpdatedAlbum_IfAlbumIsValid_AndFileIsNotNull()
    {
        // Arrange
        string mockVisualsFolder = "testFolder";
        string mockOldFilePath = "path/to/old/visual";
        string mockNewFilePath = "path/to/new/visual";
        Guid id = Guid.NewGuid();

        var newAlbum = new Album
        {
            Id = id,
            AlbumTitle = "honky_dory",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        var album = new Album
        {
            Id = id,
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            AlbumVisualPath = mockOldFilePath,
        };

        AlbumDto albumDtoUpdate = new AlbumDto
        {
            Id = id,
            AlbumTitle = "honky_dory",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockAlbumRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(album);
        _mockAlbumRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Album>()))
            .ReturnsAsync(newAlbum);
        _mockFileHelpers
            .Setup(fh =>
                fh.UpdateFileAsync(It.IsAny<IFormFile>(), mockOldFilePath, mockVisualsFolder)
            )
            .ReturnsAsync(mockNewFilePath);

        // Act
        var expectedAlbum = await _albumService.UpdateWithFileAsync(
            id,
            albumDtoUpdate,
            new Mock<IFormFile>().Object
        );

        // Assert
        Assert.Equal(album.Id, expectedAlbum?.Id);
        Assert.Equal(album.AlbumTitle, expectedAlbum?.AlbumTitle);
        Assert.Equal(album.AlbumVisualPath, expectedAlbum?.AlbumVisualPath);
        Assert.Equal(album.UserId, expectedAlbum?.UserId);
    }

    [Fact]
    public async Task UpdateWithFileAsync_ShouldReturnNull_IfExistingAlbumIsNull()
    {
        // Arrange
        Guid newAlbumId = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79");
        AlbumDto albumDtoUpdate = new AlbumDto();

        // Act
        var result = await _albumService.UpdateWithFileAsync(newAlbumId, albumDtoUpdate, null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateWithFileAsync_ShouldCallUpdateFileAsync_IfAlbumVisualIsNotNull()
    {
        // Arrange
        string mockVisualsFolder = "testFolder";
        string mockOldFilePath = "path/to/old/visual";
        string mockNewFilePath = "path/to/new/visual";
        Guid id = Guid.NewGuid();

        var newAlbum = new Album
        {
            Id = id,
            AlbumTitle = "honky_dory",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };
        var album = new Album
        {
            Id = id,
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            AlbumVisualPath = mockOldFilePath,
        };

        AlbumDto albumDtoUpdate = new AlbumDto
        {
            AlbumTitle = "honky_dory",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockAlbumRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(album);
        _mockAlbumRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Album>()))
            .ReturnsAsync(newAlbum);
        _mockFileHelpers
            .Setup(fh =>
                fh.UpdateFileAsync(It.IsAny<IFormFile>(), mockOldFilePath, mockVisualsFolder)
            )
            .ReturnsAsync(mockNewFilePath);

        // Act
        await _albumService.UpdateWithFileAsync(id, albumDtoUpdate, new Mock<IFormFile>().Object);

        // Assert
        _mockFileHelpers.Verify(
            fh => fh.UpdateFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateWithFileAsync_ShouldReturnNull_IfMappedAlbumIsNull()
    {
        // Arrange
        string mockVisualsFolder = "testFolder";
        string mockOldFilePath = "path/to/old/visual";
        string mockNewFilePath = "path/to/new/visual";
        Guid id = Guid.NewGuid();

        var album = new Album
        {
            Id = id,
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
            AlbumVisualPath = mockOldFilePath,
        };

        AlbumDto albumDtoUpdate = new AlbumDto
        {
            AlbumTitle = "honky_dory",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockAlbumRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(album);
        _mockAlbumRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Album>()))
            .ReturnsAsync((Album?)null);
        _mockFileHelpers
            .Setup(fh =>
                fh.UpdateFileAsync(It.IsAny<IFormFile>(), mockOldFilePath, mockVisualsFolder)
            )
            .ReturnsAsync(mockNewFilePath);

        // Act
        var result = await _albumService.UpdateWithFileAsync(
            id,
            albumDtoUpdate,
            new Mock<IFormFile>().Object
        );

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateWithFileAsync_ShouldCallSaveFileAsync_WhenAlbumVisualPathIsNull()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var album = new Album { Id = albumId, AlbumVisualPath = null }; // Ensure AlbumVisualPath is null
        var mockFile = new Mock<IFormFile>();
        var mockFolder = "testFolder";
        var mockNewFilePath = "new/path/to/file.jpg";
        var albumDtoUpdate = new AlbumDto { Id = albumId, AlbumTitle = "Updated Title", AlbumVisualPath = mockNewFilePath,  };


        _mockAlbumRepository.Setup(repo => repo.GetByIdAsync(albumId)).ReturnsAsync(album);
        _mockFileHelpers
            .Setup(fh => fh.SaveFileAsync(It.IsAny<IFormFile>(), mockFolder))
            .ReturnsAsync(mockNewFilePath);

        // Act
        await _albumService.UpdateWithFileAsync(albumId, albumDtoUpdate, mockFile.Object);

        // Assert
        _mockFileHelpers.Verify(
            fh => fh.SaveFileAsync(It.IsAny<IFormFile>(), mockFolder),
            Times.Once
        );
        Assert.Equal(mockNewFilePath, album.AlbumVisualPath); // Ensure the new path is set correctly
    }

    /********** Delete **********/
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfAlbumExists()
    {
        // Arrange
        var album = new Album
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockAlbumRepository.Setup(repo => repo.GetByIdAsync(album.Id)).ReturnsAsync(album);
        _mockAlbumRepository.Setup(x => x.DeleteAsync(album.Id)).ReturnsAsync(true);

        // Act
        var result = await _albumService.DeleteAsync(album.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_IfExistingAlbumIsNull()
    {
        // Arrange
        var album = new Album
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            AlbumTitle = "ziggy_stardust_and_the_spiders_from_mars",
            AlbumVisualPath = "/test/file/path/ziggy_stardust_and_the_spiders_from_mars",
            UserId = "46b0f619-f02d-4d31-823b-465cfb01cae4",
        };

        _mockAlbumRepository.Setup(x => x.GetByIdAsync(album.Id)).ReturnsAsync((Album?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _albumService.DeleteAsync(album.Id));
    }
}
