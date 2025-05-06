// <copyright file="TrackService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Core.DTOs.ArtistDTOs;
using Core.DTOs.TrackArtistContributionDto;
using Core.DTOs.TrackDTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Specifications;
using Infrastructure.Helpers;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class TrackService(
    ITrackRepository trackRepository,
    IArtistService artistService,
    IAlbumService albumService,
    IGenreService genreService,
    IMapper mapper,
    IOptions<FileStorageSettings> fileStorageSettings,
    IFileHelpers fileHelpers,
    IFileValidationService fileValidationService
)
    : GenericMultiFileService<Track, TrackDto>(
        trackRepository,
        mapper,
        fileHelpers
    ),
        ITrackService
{
    public async Task<TrackDto?> CreateWithAudioFileAsync(TrackCreateDto trackCreateDto, IFormFile file)
    {
        string fullAudioFilePath = null;
        if (file != null)
        {
            string audioFolder = GetAudioFolder();
            fullAudioFilePath = await fileHelpers.SaveFileAsync(file, audioFolder);
        }

        var track = mapper.Map<Track>(trackCreateDto);

        if (fullAudioFilePath != null)
        {
            SetAudioFilePath(track, fullAudioFilePath);
        }

        var createdTrack = await trackRepository.CreateAsync(track);

        if (createdTrack == null)
        {
            return null;
        }

        return mapper.Map<TrackDto>(createdTrack);
    }


    public async Task<List<TrackDto>> GetByUserIdAsync(string userId)
    {
        var specs = new TrackSpecification(userId);
        var tracks = await trackRepository.GetEntityListBySpecificationAsync(specs);
        return mapper.Map<List<TrackDto>>(tracks);
    }

    public async Task<IReadOnlyList<TrackCardDto>> GetTrackCardListByUserIdAsync(
        TrackSpecification specs
    )
    {
        return await trackRepository.GetCardListByUserIdAsync(specs);
    }

    public async Task<TrackCardDto?> HandleTrackCardPostAsync(
        TrackCardDto? trackCardDto,
        IFormFile audioFile,
        IFormFile? visualFile
    )
    {
        if (audioFile == null)
        {
            throw new ArgumentException("Audio file is required.");
        }

        fileValidationService.ValidateAudioFile(audioFile);

        if (visualFile != null)
        {
            fileValidationService.ValidatePictureFile(visualFile);
        }

        var trackDto = mapper.Map<TrackCreateDto>(trackCardDto);
        var albumTitle = trackCardDto?.AlbumTitle;
        var firstGenrelabel =
            trackCardDto?.FirstGenre ?? throw new ArgumentException("First genre is required.");
        var secondaryGenreLabel =
            trackCardDto?.SecondaryGenre
            ?? throw new ArgumentException("Secondary genre is required.");
        var userId = trackCardDto?.UserId ?? throw new ArgumentException("User ID is required.");
        if (albumTitle != null)
        {
            trackDto.AlbumId = await albumService.GetAlbumIdByTitleAsync(albumTitle, userId);
        }

        var track = await CreateWithAudioFileAsync(trackDto, audioFile) ?? throw new ArgumentException("Error creating track.");

        await CreateArtistsForTrack(
            track.Id,
            trackCardDto?.ArtistsMusicAndLyrics,
            "Musique et paroles"
        );
        await CreateArtistsForTrack(track.Id, trackCardDto?.ArtistsLyrics, "Paroles");
        await CreateArtistsForTrack(track.Id, trackCardDto?.ArtistsMusic, "Musique");

        var returnTrack = await GetTrackCardByTrackIdAsync(track.Id);
        return returnTrack;
    }

    private async Task CreateArtistsForTrack(Guid trackId, IList<string>? artists, string label)
    {
        if (artists == null || artists.Count == 0)
            return;

        foreach (var artist in artists)
        {
            ArtistDto artistDto = new ()
            {
                Firstname = artist.Split(" ")[0],
                Lastname = artist.Split(" ")[1],
            };

            await artistService.CreateAsync(artistDto);
        }
    }

    public async Task<TrackCardDto> GetTrackCardByTrackIdAsync(Guid id)
    {
        var specs = new TrackSpecification(id);
        return await trackRepository.GetCardBySpecificationAsync(specs);
    }

    public async Task<(FileStream?, string?)> GetPictureByTrackIdAsync(Guid id)
    {
        try
        {
            var track = await trackRepository.GetByIdAsync(id);
            if (track == null)
            {
                throw new ArgumentException($"No track found with ID {id}");
            }

            var album = await albumService.GetByIdAsync(track.Album.Id);

            var filePath =
                album.AlbumVisualPath
                ?? throw new ArgumentException("No visual file found for this track.");

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var filenameWithExtension = Path.GetFileName(filePath);
            var sanitizedFileName = filenameWithExtension.Substring(
                filenameWithExtension.IndexOf('_') + 1
            );

            return (fileStream, sanitizedFileName);
        }
        catch
        {
            return (null, null);
        }
    }

    public async Task<int> CountAsync(TrackSpecification spec)
    {
        return await trackRepository.CountAsync(spec);
    }

    protected override string GetAudioFolder()
    {
        return fileStorageSettings.Value.TrackAudioFilesFolder
            ?? throw new InvalidOperationException(
                "Track audio files folder path is not configured."
            );
    }

    protected override string GetVisualFolder()
    {
        return fileStorageSettings.Value.TrackVisualFilesFolder
            ?? throw new InvalidOperationException(
                "Track visual files folder path is not configured."
            );
    }

    protected override string GetAudioFilePath(Track entity)
    {
        return entity.AudioFilePath;
    }

    protected override string? GetVisualFilePath(Track entity)
    {
        return entity.Album.AlbumVisualPath;
    }

    protected override void SetAudioFilePath(Track entity, string filePath)
    {
        entity.AudioFilePath = filePath;
    }

    protected override void SetLength(Track entity, string length)
    {
        entity.Length = length;
    }
}
