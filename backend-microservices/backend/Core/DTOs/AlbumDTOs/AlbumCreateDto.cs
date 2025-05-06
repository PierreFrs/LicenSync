using Core.DTOs.ArtistDTOs;
using Core.DTOs.TrackDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.AlbumDTOs
{
    public class AlbumCreateDto : BaseDto
    {
        public string AlbumTitle { get; set; } = string.Empty;

        public string RecordLabel { get; set; } = string.Empty;

        public Genre? FirstGenre { get; set; }

        public Genre? SecondaryGenre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string UserId { get; set; } = string.Empty;

        public IList<ArtistCreateDto> Artists { get; set; } = new List<ArtistCreateDto>();

        public IList<TrackCreateDto> Tracks { get; set; } = new List<TrackCreateDto>();
    }
}
