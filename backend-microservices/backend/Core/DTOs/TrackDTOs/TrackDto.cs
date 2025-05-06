// Copyright : Pierre FRAISSE
// backend>backend>TrackDto.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.TrackArtistContributionDto;
using Core.DTOs.TrackerDTOs;

namespace Core.DTOs.TrackDTOs;

public class TrackDto : TrackerDto
{
    public string TrackTitle { get; set; } = string.Empty;

    public string Length { get; set; } = string.Empty;

    public string AudioFilePath { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string? RecordLabel { get; set; }

    public Guid? FirstGenreId { get; set; }

    public Guid? SecondaryGenreId { get; set; }

    public Guid? AlbumId { get; set; }

    public Guid? BlockchainHashId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public IList<TrackArtistContributionCreateDto> ArtistContributions { get; set; } = new List<TrackArtistContributionCreateDto>();
}
