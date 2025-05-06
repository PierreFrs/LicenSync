// <copyright file="AlbumService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Core.DTOs.AlbumDTOs;
using Core.DTOs.ArtistDTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Specifications;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class AlbumService(
    IAlbumRepository albumRepository,
    IArtistRepository artistRepository,
    IMapper mapper,
    IFileHelpers fileHelpers,
    IOptions<FileStorageSettings> fileStorageSettings
)
    : GenericFileService<Album, AlbumDto>(albumRepository, mapper, fileHelpers),
        IAlbumService
{
    public async Task<AlbumDto> CreateAlbumWithVisualFileAsync(AlbumCreateDto albumCreateDto, IFormFile? file)
    {
        string? fullVisualFilePath = null;
        if (file != null)
        {
            string visulaFolder = GetFolder();
            fullVisualFilePath = await fileHelpers.SaveFileAsync(file, visulaFolder);
        }

        var album = mapper.Map<Album>(albumCreateDto);

        if (fullVisualFilePath != null) 
        {
            SetFilePath(album, fullVisualFilePath);
        }

        if (albumCreateDto.Artists != null && albumCreateDto.Artists.Any())
        {
            var artistsToFind = albumCreateDto.Artists
               .Select(a => new Artist 
               { 
                   Firstname = a.Firstname, 
                   Lastname = a.Lastname
               })
               .ToList();

            var existingArtists = await artistRepository.GetListByNamesAsync(artistsToFind);

            foreach (var artistDto in albumCreateDto.Artists)
            {
                var existingArtist = existingArtists.FirstOrDefault(a =>
                string.Equals(a.Firstname, artistDto.Firstname, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(a.Lastname, artistDto.Lastname, StringComparison.CurrentCultureIgnoreCase));

                if (existingArtist != null)
                {
                    album.Artists.Add(existingArtist);
                }
                else
                {
                    var newArtist = new Artist
                    {
                        Firstname = artistDto.Firstname,
                        Lastname = artistDto.Lastname,
                        CreationDate = DateTime.UtcNow
                    };

                    album.Artists.Add(newArtist);
                }
            }
        }

        var createdAlbum = await albumRepository.CreateAsync(album);

        if (createdAlbum == null)
        {
            throw new ApplicationException("Failed to create album"); ;
        }

        return mapper.Map<AlbumDto>(createdAlbum);
    }

    public async Task<IReadOnlyList<AlbumDto?>?> GetAlbumListByUserIdAsync(string userId)
    {
        try
        {
            var specs = new AlbumSpecification(userId);

            var albums = await albumRepository.GetEntityListBySpecificationAsync(specs);
            return mapper.Map<List<AlbumDto>>(albums) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IReadOnlyList<AlbumCardDto>?> GetAlbumCardListByUserIdAsync(string userId)
    {
        var specs = new AlbumSpecification(userId);
        return await albumRepository.GetCardListByUserIdAsync(specs);
    }

    public async Task<AlbumCardDto?> GetAlbumCardByAlbumIdAsync(Guid id)
    {
        var specs = new AlbumSpecification(id);
        return await albumRepository.GetCardBySpecificationAsync(specs);
    }

    public async Task<Guid> GetAlbumIdByTitleAsync(string title, string userId)
    {
        return await albumRepository.GetAlbumIdByTitleAsync(title, userId);
    }

    protected override string GetFolder()
    {
        return fileStorageSettings.Value.AlbumVisualsFolder
            ?? throw new InvalidOperationException("Album visuals folder path is not configured.");
    }

    protected override void SetFilePath(Album entity, string filePath)
    {
        entity.AlbumVisualPath = filePath;
    }

    protected override void SetUpdateDate(Album entity)
    {
        entity.UpdateDate = DateTime.Now;
    }

    protected override string GetFilePath(Album entity)
    {
        return entity.AlbumVisualPath ?? string.Empty;
    }

    protected override string UpdateFile(Album entity, IFormFile file, string folder)
    {
        if (entity.AlbumVisualPath != null)
        {
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), entity.AlbumVisualPath);
            fileHelpers.DeleteFile(oldFilePath);
            return fileHelpers.UpdateFileAsync(file, oldFilePath, folder).Result;
        }

        return fileHelpers.SaveFileAsync(file, folder).Result;
    }
}
