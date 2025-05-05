// <copyright file="AlbumConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        new TrackerConfiguration<Album>().Configure(builder);

        builder
            .Property(a => a.AlbumTitle)
            .HasColumnName("album_title")
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(a => a.AlbumVisualPath)
            .HasColumnName("album_visual_path")
            .IsRequired(false)
            .HasMaxLength(255);

        builder
            .HasMany(a => a.Artists)
            .WithMany(a => a.Albums)
            .UsingEntity(j => j.ToTable("album_artist"));
    }
}
