// Copyright : Pierre FRAISSE
// backend>backend>Tracker.cs
// Created : 2024/05/1414 - 13:05

namespace Core.Entities;

public class Tracker : BaseEntity
{
    public DateTime CreationDate { get; init; }

    public DateTime? UpdateDate { get; set; }
}
