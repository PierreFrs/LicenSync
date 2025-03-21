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

    public string? AlbumVisualPath { get; set; }

    public ICollection<Track> Tracks { get; init; } = new List<Track>();
}
