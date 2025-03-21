// Copyright : Pierre FRAISSE
// backend>backend>ContributionDto.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.TrackerDTOs;

namespace Core.DTOs.ContributionDTOs;

public class ContributionDto : TrackerDto
{
    public string? Label { get; set; } = string.Empty;
}
