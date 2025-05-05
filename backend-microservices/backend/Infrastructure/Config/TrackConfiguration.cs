// <copyright file="TrackConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        new TrackerConfiguration<Track>().Configure(builder);

        builder
            .Property(t => t.TrackTitle)
            .HasColumnName("track_title")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Length).HasColumnName("length").IsRequired().HasMaxLength(8);

        builder
            .Property(t => t.AudioFilePath)
            .HasColumnName("audio_file_path")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.UserId).HasColumnName("user_id").IsRequired();

        builder.Property(t => t.RecordLabel).HasColumnName("record_label").HasMaxLength(50);

        builder
            .Property(t => t.TrackVisualPath)
            .HasColumnName("track_visual_path")
            .IsRequired(false)
            .HasMaxLength(255);

        builder.Property(t => t.FirstGenreId).HasColumnName("first_genre_id").IsRequired(false);

        builder
            .HasOne(t => t.FirstGenre)
            .WithMany()
            .HasForeignKey(t => t.FirstGenreId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Property(t => t.SecondaryGenreId)
            .HasColumnName("secondary_genre_id")
            .IsRequired(false);

        builder
            .HasOne(t => t.SecondaryGenre)
            .WithMany()
            .HasForeignKey(t => t.SecondaryGenreId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(t => t.AlbumId).HasColumnName("album_id").IsRequired(false);

        builder
            .HasOne(t => t.Album)
            .WithMany(a => a.Tracks)
            .HasForeignKey(t => t.AlbumId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .Property(t => t.BlockchainHashId)
            .HasColumnName("blockchain_hash_id")
            .IsRequired(false);

        builder
            .Property(t => t.ReleaseDate)
            .HasColumnName("release_date")
            .IsRequired();
    }
}
