// Copyright : Pierre FRAISSE
// backend>backend>Album.cs
// Created : 2024/05/1414 - 13:05

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("album")]
public class Album : Tracker
{
    public string AlbumTitle { get; set; } = string.Empty;

    public string UserId { get; init; } = string.Empty;

    public string RecordLabel {  get; set; } = string.Empty;

    public Guid? FirstGenreId { get; set; }

    public virtual Genre? FirstGenre { get; set; }
    
    public Guid? SecondaryGenreId { get; set; }

    public virtual Genre? SecondaryGenre { get; set; }

    public string? AlbumVisualPath { get; set; }

    public DateTime ReleaseDate { get; set; }

    public ICollection<Artist> Artists { get; set; } = [];

    public ICollection<Track> Tracks { get; init; } = [];
}
