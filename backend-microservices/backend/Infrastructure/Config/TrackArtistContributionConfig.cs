using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Config
{
    internal class TrackArtistContributionConfig : IEntityTypeConfiguration<TrackArtistContribution>
    {
        public void Configure(EntityTypeBuilder<TrackArtistContribution> builder)
        {
            new TrackerConfiguration<TrackArtistContribution>().Configure(builder);

            builder.HasKey(tac => new { tac.TrackId, tac.ArtistId, tac.ContributionId });

            builder.Property(tac => tac.TrackId).HasColumnName("track_id").IsRequired();
            
            builder.Property(tac => tac.ArtistId).HasColumnName("artist_id").IsRequired();
            
            builder.Property(tac => tac.ContributionId).HasColumnName("contribution_id").IsRequired();

            builder
                .HasOne(tac  => tac.Track)
                .WithMany(t => t.ArtistContributions)
                .HasForeignKey(tac => tac.TrackId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(tac => tac.Artist)
                .WithMany(a  => a.TrackContributions)
                .HasForeignKey(tac => tac.TrackId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(tac => tac.Contribution)
                .WithMany(c => c.TrackArtistContributions)
                .HasForeignKey(tac => tac.ContributionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
