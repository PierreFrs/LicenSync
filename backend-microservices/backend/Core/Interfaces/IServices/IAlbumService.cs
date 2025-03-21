// Copyright : Pierre FRAISSE
// backend>backend>IAlbumService.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.AlbumDTOs;
using Core.DTOs.CardDTOs;
using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IAlbumService : IGenericFileService<Album, AlbumDto>
{
    Task<IReadOnlyList<AlbumDto?>?> GetAlbumListByUserIdAsync(string userId);

    Task<IReadOnlyList<AlbumCardDto>?> GetAlbumCardListByUserIdAsync(string userId);

    Task<AlbumCardDto?> GetAlbumCardByAlbumIdAsync(Guid id);

    Task<Guid> GetAlbumIdByTitleAsync(string title, string userId);
}
