// <copyright file="ArtistConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        new TrackerConfiguration<Artist>().Configure(builder);

        builder.Property(a => a.Firstname).HasColumnName("firstname").IsRequired().HasMaxLength(50);

        builder.Property(a => a.Lastname).HasColumnName("lastname").IsRequired().HasMaxLength(50);

        builder.Property(a => a.TrackId).HasColumnName("track_id").IsRequired();

        builder
            .HasOne(a => a.Track)
            .WithMany(t => t.Artists)
            .HasForeignKey(a => a.TrackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.ContributionId).HasColumnName("contribution_id").IsRequired();

        builder
            .HasOne(a => a.Contribution)
            .WithMany(c => c.Artists)
            .HasForeignKey(a => a.ContributionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
