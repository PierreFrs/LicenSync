using Core.DTOs.ArtistDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.TrackArtistContributionDto
{
    public class TrackArtistContributionCreateDto
    {
        public Guid? ExistingArtistId { get; set; }

        public ArtistCreateDto? NewArtist { get; set; }

        public Guid ContributionId { get; set; }
    }
}
