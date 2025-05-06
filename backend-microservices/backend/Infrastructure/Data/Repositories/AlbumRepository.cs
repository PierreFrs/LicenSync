// Copyright : Pierre FRAISSE
// backend>backend>AlbumRepository.cs
// Created : 2024/05/1414 - 13:05

using Core.Entities;
using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AlbumRepository(ApplicationDbContext context) : GenericRepository<Album>(context), IAlbumRepository
{
    public async Task<Guid> GetAlbumIdByTitleAsync(string title, string UserId)
    {
        return await context
            .Albums.AsNoTracking()
            .Where(x => x.AlbumTitle == title && x.UserId == UserId && x.ReleaseDate >= DateTime.Now)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }
}
