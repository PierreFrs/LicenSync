// Copyright : Pierre FRAISSE
// backend>backend>GenreDto.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backend => GenreDto.cs
// Created : 2023/12/12 - 14:49
// Updated : 2023/12/12 - 15:25

using Core.DTOs.TrackerDTOs;

namespace Core.DTOs.GenreDTOs;

public class GenreDto : TrackerDto
{
    public string Label { get; set; } = string.Empty;
}
