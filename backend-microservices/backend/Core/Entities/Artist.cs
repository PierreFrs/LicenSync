// Copyright : Pierre FRAISSE
// backend>backend>Artist.cs
// Created : 2024/05/1414 - 13:05

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("artist")]
public class Artist : Tracker
{
    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public Guid TrackId { get; set; } = Guid.Empty;

    public Track Track { get; init; } = null!;

    public Guid ContributionId { get; set; } = Guid.Empty;

    public Contribution Contribution { get; init; } = null!;
}
