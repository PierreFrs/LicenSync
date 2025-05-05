using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IRepositories
{
    public interface ITrackArtistContributionRepository : IGenericRepository<TrackArtistContribution>
    {
        Task<IReadOnlyList<TrackArtistContribution>> GetContributionsByTrackIdAsync(Guid trackId);

        Task<IReadOnlyList<TrackArtistContribution>> GetContributionsByArtistIdAsync(Guid artistId);

        Task<bool> DeleteContributionsForTrackAsync(Guid trackId);
    }
}
