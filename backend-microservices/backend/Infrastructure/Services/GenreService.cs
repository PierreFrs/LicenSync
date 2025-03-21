// <copyright file="GenreService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Core.DTOs.GenreDTOs;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Infrastructure.Services;

public class GenreService(IGenreRepository genreRepository, IMapper mapper)
    : GenericService<Genre, GenreDto>(genreRepository, mapper),
        IGenreService
{
    public async Task<Guid> GetGenreIdByLabelAsync(string label)
    {
        return await genreRepository.GetGenreIdByLabelAsync(label);
    }
}
