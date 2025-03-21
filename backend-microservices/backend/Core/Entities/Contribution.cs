// Copyright : Pierre FRAISSE
// backend>backend>Contribution.cs
// Created : 2024/05/1414 - 13:05

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("contribution")]
public class Contribution : Tracker
{
    public string Label { get; set; } = string.Empty;

    public virtual ICollection<Artist>? Artists { get; init; }
}
