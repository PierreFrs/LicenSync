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

    public virtual ICollection<Album> Albums { get; set; } = [];

    public virtual ICollection<TrackArtistContribution> TrackContributions { get; set; } = [];
}
