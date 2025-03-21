// <copyright file="GenreConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        new TrackerConfiguration<Genre>().Configure(builder);

        builder.Property(g => g.Label).HasColumnName("label").IsRequired().HasMaxLength(50);
    }
}
