// Copyright : Pierre FRAISSE
// backend>backend>Track.cs
// Created : 2024/05/1414 - 13:05

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("track")]
public class Track : Tracker
{
    public string TrackTitle { get; set; } = string.Empty;

    public string Length { get; set; } = string.Empty;

    public string AudioFilePath { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string? RecordLabel { get; set; }

    public string? TrackVisualPath { get; set; }

    public Guid? FirstGenreId { get; set; }

    public virtual Genre? FirstGenre { get; set; }

    public Guid? SecondaryGenreId { get; set; }

    public virtual Genre? SecondaryGenre { get; set; }

    public Guid? AlbumId { get; set; }

    public virtual Album? Album { get; set; }

    public Guid? BlockchainHashId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public virtual ICollection<TrackArtistContribution> ArtistContributions { get; set; } = new List<TrackArtistContribution>();
}
