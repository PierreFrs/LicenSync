using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("track_artist_contribution")]
    public class TrackArtistContribution : Tracker
    {
        public Guid TrackId { get; set; }
        public virtual Track Track { get; set; } = new Track();

        public Guid ArtistId { get; set; }
        public virtual Artist Artist { get; set; } = new Artist();

        public Guid ContributionId { get; set; }
        public virtual Contribution Contribution { get; set; } = new Contribution();
    }
}
