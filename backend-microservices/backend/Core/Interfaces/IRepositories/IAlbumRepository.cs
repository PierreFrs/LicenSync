// Copyright : Pierre FRAISSE
// backend>backend>IAlbumRepository.cs
// Created : 2024/05/1414 - 13:05

using Core.Entities;

namespace Core.Interfaces.IRepositories;

public interface IAlbumRepository : IGenericRepository<Album>
{
    Task<Guid> GetAlbumIdByTitleAsync(string title, string UserId);
}
