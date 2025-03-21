// Copyright : Pierre FRAISSE
// backend>backend>TrackRepository.cs
// Created : 2024/05/1414 - 13:05

using Core.Entities;
using Core.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class TrackRepository(ApplicationDbContext context)
    : GenericRepository<Track>(context),
        ITrackRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<Track> StoreHashGuidInDatabaseAsync(Guid trackId, Guid hashId)
    {
        var track = await context.Tracks.FindAsync(trackId);
        if (track == null)
        {
            throw new ArgumentException($"No track found with ID {trackId}");
        }

        track.BlockchainHashId = hashId;

        await context.SaveChangesAsync();
        return track;
    }
}
