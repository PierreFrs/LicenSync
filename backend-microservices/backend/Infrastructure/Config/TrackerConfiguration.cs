// <copyright file="TrackerConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class TrackerConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Tracker
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        new BaseEntityConfiguration<TEntity>().Configure(builder);

        builder
            .Property(t => t.CreationDate)
            .HasColumnName("creation_date")
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(t => t.UpdateDate).HasColumnName("update_date").IsRequired(false);
    }
}
