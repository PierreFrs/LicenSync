// Copyright : Pierre FRAISSE
// backend>backendTests>ArtistServiceTests.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.ArtistDTOs;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Specifications;
using Infrastructure.Services;
using Moq;

namespace backendTests.ServiceTests;

[Collection("ServiceTests")]
public class ArtistServiceTests
{
    private readonly Mock<IArtistRepository> _mockArtistRepository;
    private readonly Mock<ITrackRepository> _mockTrackRepository;
    private readonly ArtistService _artistService;

    public ArtistServiceTests()
    {
        _mockArtistRepository = new Mock<IArtistRepository>();
        _mockTrackRepository = new Mock<ITrackRepository>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Artist, ArtistDto>().ReverseMap();
        });
        var mapper = mapperConfig.CreateMapper();
        _artistService = new ArtistService(
            _mockArtistRepository.Object,
            _mockTrackRepository.Object,
            mapper
        );
    }

    /********* Getters *********/
    [Fact]
    public async Task GetListAsync_ShouldReturnListOfArtists()
    {
        // Arrange
        var artistList = new List<Artist>
        {
            new ()
            {
                Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
                Firstname = "David",
                Lastname = "Bowie",
            },
            new ()
            {
                Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
                Firstname = "David",
                Lastname = "Bowie",
            },
        };
        _mockArtistRepository.Setup(repo => repo.GetListAsync()).ReturnsAsync(artistList);

        // Act
        var expectedList = await _artistService.GetListAsync();

        // Assert
        Assert.Equal(artistList.Count, expectedList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnArtist_IfIdIsValid()
    {
        // Arrange
        var artist = new Artist
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Firstname = "David",
            Lastname = "Bowie",
        };
        _mockArtistRepository.Setup(repo => repo.GetByIdAsync(artist.Id)).ReturnsAsync(artist);

        // Act
        var expectedArtist = await _artistService.GetByIdAsync(artist.Id);

        // Assert
        if (expectedArtist != null)
            Assert.Equal(artist.Id, expectedArtist.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_IfIdIsInvalid()
    {
        // Arrange

        // Act
        var expectedArtist = await _artistService.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(expectedArtist);
    }

    [Fact]
    public async Task GetByTrackIdAsync_ShouldReturnListOfArtists_IfIdIsValid()
    {
        // Arrange
        var trackId = Guid.NewGuid();

        var artistList = new List<Artist>
        {
            new ()
            {
                Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"), 
                Firstname = "David", 
                Lastname = "Bowie"
            },
            new() {
                Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
                Firstname = "David",
                Lastname = "Bowie",
            },
        };

        foreach (var artist in artistList)
        {
            artist.TrackContributions =
            [
                new ()
                {
                    ArtistId = artist.Id,
                    TrackId = trackId,
                    ContributionId = Guid.NewGuid()
                }
            ];
        }

        var track = new Track { Id = trackId };

        _mockTrackRepository.Setup(repo => repo.GetByIdAsync(trackId)).ReturnsAsync(track);

        _mockArtistRepository
            .Setup(repo => repo.GetEntityListBySpecificationAsync(It.IsAny<ArtistSpecification>()))
            .ReturnsAsync(artistList);

        // Act
        var result = await _artistService.GetArtistsByTrackIdAsync(trackId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(artistList.Count, result.Count);


        var firstArtist = result[0];
        Assert.NotNull(firstArtist);
        Assert.Equal("David", firstArtist.Firstname);
        Assert.Equal("Bowie", firstArtist.Lastname);
    }

    [Fact]
    public async Task GetArtistsByTrackIdAsync_ShouldReturnMappedArtistsList_WhenTrackIdIsValid()
    {
        // Arrange
        var trackId = Guid.NewGuid();
        var artists = new List<Artist>
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Firstname = "John",
                Lastname = "Doe",
            },
            new ()
            {
                Id = Guid.NewGuid(),
                Firstname = "Jane",
                Lastname = "Smith",
            },
        };
        var artistDtos = new List<ArtistDto>
        {
            new ()
            {
                Id = artists[0].Id,
                Firstname = "John",
                Lastname = "Doe",
            },
            new ()
            {
                Id = artists[1].Id,
                Firstname = "Jane",
                Lastname = "Smith",
            },
        };

        _mockTrackRepository.Setup(repo => repo.GetByIdAsync(trackId)).ReturnsAsync(new Track());
        _mockArtistRepository
            .Setup(repo => repo.GetEntityListBySpecificationAsync(It.IsAny<ArtistSpecification>()))
            .ReturnsAsync(artists);

        // Act
        var result = await _artistService.GetArtistsByTrackIdAsync(trackId);

        // Assert
        Assert.NotNull(result);
        if (result != null)
        {
            Assert.Equal(artistDtos.Count, result.Count);
            Assert.NotNull(result[0]);
            Assert.NotNull(result[1]);
            var artist0 = result[0];
            var artist1 = result[1];
            Assert.Equal(artistDtos[0]?.Id, artist0?.Id);
            Assert.Equal(artistDtos[1]?.Id, artist1?.Id);
        }
    }

    [Fact]
    public async Task GetArtistsByTrackIdAsync_ShouldReturnEmptyList_WhenTrackIdIsInvalid()
    {
        // Arrange
        var trackId = Guid.NewGuid();

        _mockTrackRepository.Setup(repo => repo.GetByIdAsync(trackId)).ReturnsAsync((Track?)null);

        // Act
        var result = await _artistService.GetArtistsByTrackIdAsync(trackId);

        // Assert
        Assert.Null(result);
    }

    /********* Create *********/

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedArtist_IfArtistIsValid()
    {
        // Arrange
        var artistDto = new ArtistDto
        {
            Firstname = "David",
            Lastname = "Bowie",
        };

        var artist = new Artist
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Firstname = "David",
            Lastname = "Bowie",
        };

        _mockArtistRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<Artist>()))
            .ReturnsAsync(artist);

        // Act
        var expectedArtist = await _artistService.CreateAsync(artistDto);

        // Assert
        if (expectedArtist != null)
        {
            Assert.Equal(artist.Id, expectedArtist.Id);
            Assert.Equal(artist.Firstname, expectedArtist.Firstname);
            Assert.Equal(artist.Lastname, expectedArtist.Lastname);
        }
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnNull_IfArtistIsNull()
    {
        // Arrange
        var artistDto = new ArtistDto();
        var artist = new Artist();
        _mockArtistRepository.Setup(x => x.CreateAsync(artist)).ReturnsAsync((Artist?)null);

        // Act
        var expectedArtist = await _artistService.CreateAsync(artistDto);

        // Assert
        Assert.Null(expectedArtist);
    }

    /********* Update *********/

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedArtist_IfArtistIsValid()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        var artist = new Artist
        {
            Id = id,
            Firstname = "David",
            Lastname = "Bowie",
        };

        var newArtist = new Artist
        {
            Id = id,
            Firstname = "David",
            Lastname = "Bowie",
        };

        ArtistDto artistDtoUpdate = new ()
        {
            Id = id,
            Firstname = newArtist.Firstname,
            Lastname = newArtist.Lastname,
        };

        _mockArtistRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(artist);
        _mockArtistRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Artist>()))
            .ReturnsAsync(newArtist);

        // Act

        var expectedArtist = await _artistService.UpdateAsync(id, artistDtoUpdate);

        // Assert
        if (expectedArtist != null)
        {
            Assert.Equal(artist.Id, expectedArtist.Id);
            Assert.Equal(artist.Firstname, expectedArtist.Firstname);
            Assert.Equal(artist.Lastname, expectedArtist.Lastname);
        }
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_IfExistingArtistIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        ArtistDto artistDtoUpdate = new ()
        {
            Firstname = "David",
            Lastname = "Bowie",
        };

        _mockArtistRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Artist?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _artistService.UpdateAsync(id, artistDtoUpdate)
        );
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_IfUpdatedArtistIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var artist = new Artist
        {
            Id = id,
            Firstname = "David",
            Lastname = "Bowie",
        };

        var newArtist = new Artist
        {
            Id = id,
            Firstname = "David",
            Lastname = "Bowie",
        };

        ArtistDto artistDtoUpdate = new ()
        {
            Firstname = newArtist.Firstname,
            Lastname = newArtist.Lastname,
        };

        _mockArtistRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(artist);
        _mockArtistRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<Artist>()))
            .ReturnsAsync((Artist?)null);

        // Act
        var expectedArtist = await _artistService.UpdateAsync(id, artistDtoUpdate);

        // Assert
        Assert.Null(expectedArtist);
    }

    /********* Delete *********/

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfArtistIsValid()
    {
        // Arrange
        var artist = new Artist
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Firstname = "David",
            Lastname = "Bowie",
        };

        _mockArtistRepository.Setup(repo => repo.GetByIdAsync(artist.Id)).ReturnsAsync(artist);
        _mockArtistRepository.Setup(repo => repo.DeleteAsync(artist.Id)).ReturnsAsync(true);

        // Act
        var expectedArtist = await _artistService.DeleteAsync(artist.Id);

        // Assert
        Assert.True(expectedArtist);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_ifExistingArtistIsNull()
    {
        // Arrange
        var artist = new Artist
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Firstname = "David",
            Lastname = "Bowie",
        };

        _mockArtistRepository.Setup(x => x.GetByIdAsync(artist.Id)).ReturnsAsync((Artist?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _artistService.DeleteAsync(artist.Id));
    }
}
