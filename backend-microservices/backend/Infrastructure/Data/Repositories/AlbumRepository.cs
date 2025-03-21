// Copyright : Pierre FRAISSE
// backend>backend>AlbumRepository.cs
// Created : 2024/05/1414 - 13:05

using Core.Entities;
using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{
    private readonly ApplicationDbContext _context;
    
    public AlbumRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Guid> GetAlbumIdByTitleAsync(string title, string UserId)
    {
        return await _context
            .Albums.AsNoTracking()
            .Where(x => x.AlbumTitle == title && x.UserId == UserId)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }
}
