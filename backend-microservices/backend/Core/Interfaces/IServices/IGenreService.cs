// Copyright : Pierre FRAISSE
// backend>backend>IGenreService.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.GenreDTOs;
using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IGenreService : IGenericService<Genre, GenreDto>
{
    Task<Guid> GetGenreIdByLabelAsync(string label);
}
