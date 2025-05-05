// <copyright file="ContributionConfig.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ContributionConfig : IEntityTypeConfiguration<Contribution>
{
    public void Configure(EntityTypeBuilder<Contribution> builder)
    {
        new TrackerConfiguration<Contribution>().Configure(builder);

        builder.Property(c => c.Label)
            .HasColumnName("label")
            .IsRequired()
            .HasMaxLength(50);
    }
}
