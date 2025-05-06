// Copyright : Pierre FRAISSE
// backend>backend>IAlbumService.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.AlbumDTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.IServices;

public interface IAlbumService : IGenericFileService<Album, AlbumDto>
{
    Task<AlbumDto?> CreateAlbumWithVisualFileAsync(AlbumCreateDto albumCreateDto, IFormFile? file);

    Task<IReadOnlyList<AlbumDto?>?> GetAlbumListByUserIdAsync(string userId);

    Task<IReadOnlyList<AlbumCardDto>?> GetAlbumCardListByUserIdAsync(string userId);

    Task<AlbumCardDto?> GetAlbumCardByAlbumIdAsync(Guid id);

    Task<Guid> GetAlbumIdByTitleAsync(string title, string userId);
}
