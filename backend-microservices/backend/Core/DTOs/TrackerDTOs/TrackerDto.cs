// Copyright : Pierre FRAISSE
// backend>backend>TrackerDto.cs
// Created : 2024/05/1414 - 13:05

namespace Core.DTOs.TrackerDTOs;

public class TrackerDto : BaseDto
{
    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
