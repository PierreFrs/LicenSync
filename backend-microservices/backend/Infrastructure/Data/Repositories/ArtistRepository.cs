// Copyright : Pierre FRAISSE
// backend>backend>ArtistRepository.cs
// Created : 2024/05/1414 - 13:05

using Core.Entities;
using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ArtistRepository(ApplicationDbContext context)
    : GenericRepository<Artist>(context),
        IArtistRepository
{
    public async Task<IReadOnlyList<Artist>?> GetListByNamesAsync(IReadOnlyList<Artist> artists)
    {
        var foundArtists = await context.Artists
            .Where(a => artists.Any(artist =>
                a.Firstname.Equals(artist.Firstname, StringComparison.CurrentCultureIgnoreCase) &&
                a.Lastname.Equals(artist.Lastname, StringComparison.CurrentCultureIgnoreCase)))
            .ToListAsync();

        return foundArtists;
    }
}
