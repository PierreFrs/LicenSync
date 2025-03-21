// Copyright : Pierre FRAISSE
// backend>backendTests>GenreServiceTests.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.GenreDTOs;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.Services;
using Moq;

namespace backendTests.ServiceTests;

[Collection("ServiceTests")]
public class GenreServiceTests
{
    private readonly Mock<IGenreRepository> _mockGenreRepository;
    private readonly GenreService _genreService;

    public GenreServiceTests()
    {
        _mockGenreRepository = new Mock<IGenreRepository>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Genre, GenreDto>().ReverseMap();
            cfg.CreateMap<GenreDto, Genre>()
                .ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
        });
        var mapper = mapperConfig.CreateMapper();
        _genreService = new GenreService(_mockGenreRepository.Object, mapper);
    }

    /********* Getters *********/
    [Fact]
    public async Task GetListAsync_ShouldReturnListOfGenres()
    {
        // Arrange
        var genreList = new List<Genre>
        {
            new Genre { Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"), Label = "Test" },
            new Genre { Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"), Label = "Test" },
        };
        _mockGenreRepository.Setup(repo => repo.GetListAsync()).ReturnsAsync(genreList);

        // Act
        var expectedList = await _genreService.GetListAsync();

        // Assert
        Assert.Equal(genreList.Count, expectedList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnGenre_IfIdIsValid()
    {
        // Arrange
        var genre = new Genre
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };
        _mockGenreRepository.Setup(repo => repo.GetByIdAsync(genre.Id)).ReturnsAsync(genre);

        // Act
        var expectedGenre = await _genreService.GetByIdAsync(genre.Id);

        // Assert
        if (expectedGenre != null)
            Assert.Equal(genre.Id, expectedGenre.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_IfIdIsInvalid()
    {
        // Arrange

        // Act
        var expectedGenre = await _genreService.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(expectedGenre);
    }

    /********* Create *********/

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedGenre_IfGenreIsValid()
    {
        // Arrange
        var genreDto = new GenreDto { Label = "Test" };

        var genre = new Genre
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        _mockGenreRepository.Setup(repo => repo.CreateAsync(It.IsAny<Genre>())).ReturnsAsync(genre);

        // Act
        var expectedGenre = await _genreService.CreateAsync(genreDto);

        // Assert
        if (expectedGenre != null)
        {
            Assert.Equal(genre.Id, expectedGenre.Id);
            Assert.Equal(genre.Label, expectedGenre.Label);
        }
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnNull_IfGenreIsNull()
    {
        // Arrange
        Genre genre = new Genre();
        GenreDto genreDto = new GenreDto();
        _mockGenreRepository.Setup(repo => repo.CreateAsync(genre)).ReturnsAsync((Genre?)null);

        // Act
        var expectedGenre = await _genreService.CreateAsync(genreDto);

        // Assert
        Assert.Null(expectedGenre);
    }

    /********* Update *********/

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedGenre_IfGenreIsValid()
    {
        // Arrange
        var genre = new Genre
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        var newGenre = new GenreDto { Label = "Test" };

        _mockGenreRepository.Setup(repo => repo.GetByIdAsync(genre.Id)).ReturnsAsync(genre);
        _mockGenreRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Genre>())).ReturnsAsync(genre);

        // Act

        var expectedGenre = await _genreService.UpdateAsync(genre.Id, newGenre);

        // Assert
        if (expectedGenre != null)
        {
            Assert.Equal(genre.Id, expectedGenre.Id);
            Assert.Equal(genre.Label, expectedGenre.Label);
        }
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_IfGenreIsNull()
    {
        // Arrange
        var genre = new Genre
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        var newGenre = new GenreDto { Label = "Test" };

        _mockGenreRepository.Setup(repo => repo.GetByIdAsync(genre.Id)).ReturnsAsync((Genre?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _genreService.UpdateAsync(genre.Id, newGenre)
        );
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_IfCreatedGenreIsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var genre = new Genre { Id = id, Label = "Test" };

        var newGenre = new GenreDto { Label = "Test" };

        _mockGenreRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(genre);
        _mockGenreRepository.Setup(repo => repo.UpdateAsync(genre)).ReturnsAsync((Genre?)null);

        // Act
        var expectedGenre = await _genreService.UpdateAsync(id, newGenre);

        // Assert
        Assert.Null(expectedGenre);
    }

    /********* Delete *********/

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_IfGenreIsValid()
    {
        // Arrange
        var genre = new Genre
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        _mockGenreRepository.Setup(repo => repo.GetByIdAsync(genre.Id)).ReturnsAsync(genre);
        _mockGenreRepository.Setup(repo => repo.DeleteAsync(genre.Id)).ReturnsAsync(true);

        // Act
        var expectedGenre = await _genreService.DeleteAsync(genre.Id);

        // Assert
        Assert.True(expectedGenre);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_IfGenreIsNull()
    {
        // Arrange
        var genre = new Genre
        {
            Id = new Guid("db06174b-2f0b-4fef-b41f-8550039b6a79"),
            Label = "Test",
        };

        _mockGenreRepository.Setup(repo => repo.GetByIdAsync(genre.Id)).ReturnsAsync((Genre?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _genreService.DeleteAsync(genre.Id));
    }
}
