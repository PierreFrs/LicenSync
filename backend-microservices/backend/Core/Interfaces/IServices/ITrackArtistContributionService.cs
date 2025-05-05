using Core.DTOs.TrackArtistContributionDto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IServices
{
    public interface ITrackArtistContributionService : IGenericService<TrackArtistContribution, TrackArtistContributionDto>
    {
        Task<IReadOnlyList<TrackArtistContributionDto>> GetContributionsByTrackIdAsync(Guid trackId);

        Task<IReadOnlyList<TrackArtistContributionDto>> GetContributionsByArtistIdAsync(Guid artistId);

        Task<bool> DeleteContributionsForTrackAsync(Guid trackId);
    }
}
