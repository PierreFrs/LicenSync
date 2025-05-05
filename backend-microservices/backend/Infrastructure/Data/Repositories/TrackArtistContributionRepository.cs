using Core.Entities;
using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using NBitcoin.Secp256k1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TrackArtistContributionRepository(ApplicationDbContext context) : GenericRepository<TrackArtistContribution>(context), ITrackArtistContributionRepository
    {
        public async Task<IReadOnlyList<TrackArtistContribution>> GetContributionsByTrackIdAsync(Guid trackId)
        {
            return await context.TrackArtistContributions
                .Include(tac => tac.Artist)
                .Include(tac => tac.Contribution)
                .Where(tac => tac.TrackId == trackId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<TrackArtistContribution>> GetContributionsByArtistIdAsync(Guid artistId)
        {
            return await context.TrackArtistContributions
                .Include(tac => tac.Track)
                .Include(tac => tac.Contribution)
                .Where(tac => tac.ArtistId == artistId)
                .ToListAsync();
        }

        public async Task<bool> DeleteContributionsForTrackAsync(Guid trackId)
        {
            var contributions = await context.TrackArtistContributions
                .Where(tac => tac.TrackId == trackId)
                .ToListAsync();

            if (contributions.Count == 0)
                return false;

            context.TrackArtistContributions.RemoveRange(contributions);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
