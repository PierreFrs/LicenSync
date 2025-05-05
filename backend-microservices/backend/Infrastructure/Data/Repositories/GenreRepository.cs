// Copyright : Pierre FRAISSE
// backend>backend>GenreRepository.cs
// Created : 2024/05/1414 - 13:05

using Core.Entities;
using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class GenreRepository(ApplicationDbContext context) : GenericRepository<Genre>(context), IGenreRepository
{
    public async Task<Guid> GetGenreIdByLabelAsync(string label)
    {
        return await context
            .Genres.Where(g => g.Label == label)
            .Select(g => g.Id)
            .FirstOrDefaultAsync();
    }
}
