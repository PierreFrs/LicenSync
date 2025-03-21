// Copyright : Pierre FRAISSE
// backend>backend>IArtistService.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.ArtistDTOs;
using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IArtistService : IGenericService<Artist, ArtistDto>
{
    Task<IReadOnlyList<ArtistDto?>?> GetArtistsByTrackIdAsync(Guid id);
}
