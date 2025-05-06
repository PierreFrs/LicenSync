using Core.DTOs.TrackArtistContributionDto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.TrackDTOs
{
    public class TrackCreateDto
    {
        public string TrackTitle { get; set; } = string.Empty;

        public string Length { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string? RecordLabel { get; set; }

        public Guid? FirstGenreId { get; set; }

        public Guid? SecondaryGenreId { get; set; }

        public Guid? AlbumId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IList<TrackArtistContributionCreateDto> ArtistContributions { get; set; } = [];
    }
}
