// Copyright : Pierre FRAISSE
// backend>backend>Genre.cs
// Created : 2024/05/1414 - 13:05

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("genre")]
public class Genre : Tracker
{
    public string Label { get; set; } = string.Empty;
}
