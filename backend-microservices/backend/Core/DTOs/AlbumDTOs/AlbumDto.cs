// Copyright : Pierre FRAISSE
// backend>backend>AlbumDto.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.TrackerDTOs;

namespace Core.DTOs.AlbumDTOs;

public class AlbumDto : TrackerDto
{
    public string AlbumTitle { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string? AlbumVisualPath { get; set; }
}
