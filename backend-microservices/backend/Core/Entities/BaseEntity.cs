// Copyright : Pierre FRAISSE
// backend>backend>BaseEntity.cs
// Created : 2024/05/1414 - 13:05

namespace Core.Entities;

public abstract class BaseEntity
{
    public virtual Guid Id { get; init; }
}
