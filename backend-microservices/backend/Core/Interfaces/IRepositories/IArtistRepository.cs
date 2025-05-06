// Copyright : Pierre FRAISSE
// backend>backend>IArtistRepository.cs
// Created : 2024/05/1414 - 13:05

using Core.Entities;

namespace Core.Interfaces.IRepositories;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task<IReadOnlyList<Artist>?> GetListByNamesAsync(IReadOnlyList<Artist> artists);
}
