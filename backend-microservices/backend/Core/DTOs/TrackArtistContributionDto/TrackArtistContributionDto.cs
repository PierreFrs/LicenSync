using Core.DTOs.TrackerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.TrackArtistContributionDto
{
    public class TrackArtistContributionDto : TrackerDto
    {
        public Guid TrackId { get; set; }

        public Guid ArtistId { get; set; }

        public Guid ContributionId { get; set; }
    }
}
