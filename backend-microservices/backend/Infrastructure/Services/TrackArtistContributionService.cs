using AutoMapper;
using Core.DTOs.TrackArtistContributionDto;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal class TrackArtistContributionService(
        ITrackArtistContributionRepository trackArtistContributionRepository,
        IMapper mapper) 
        : GenericService<TrackArtistContribution, TrackArtistContributionDto>(trackArtistContributionRepository, mapper), ITrackArtistContributionService
    {
        public async Task<IReadOnlyList<TrackArtistContributionDto>> GetContributionsByTrackIdAsync(Guid trackId)
        {
            var contributions = await trackArtistContributionRepository.GetContributionsByArtistIdAsync(trackId);

            return mapper.Map<IReadOnlyList<TrackArtistContributionDto>>(contributions);
        }

        public async Task<IReadOnlyList<TrackArtistContributionDto>> GetContributionsByArtistIdAsync(Guid artistId)
        {
            var contributions = await trackArtistContributionRepository.GetContributionsByArtistIdAsync(artistId);

            return mapper.Map<IReadOnlyList<TrackArtistContributionDto>>(contributions);
        }

        public Task<bool> DeleteContributionsForTrackAsync(Guid trackId)
        {
            return trackArtistContributionRepository.DeleteAsync(trackId);
        }
    }
}
