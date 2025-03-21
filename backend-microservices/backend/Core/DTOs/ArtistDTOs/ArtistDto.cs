// Copyright : Pierre FRAISSE
// backend>backend>ArtistDto.cs
// Created : 2024/05/1414 - 13:05


using Core.DTOs.TrackerDTOs;

namespace Core.DTOs.ArtistDTOs;

public class ArtistDto : TrackerDto
{
    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public Guid TrackId { get; set; }

    public Guid ContributionId { get; set; }
}
