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

        builder
            .HasMany(a => a.Albums)
            .WithMany(a => a.Artists)
            .UsingEntity(j => j.ToTable("album_artist"));
    }
}
