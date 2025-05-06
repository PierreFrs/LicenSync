// Copyright : Pierre FRAISSE
// backend>backend>ITrackService.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.TrackDTOs;
using Core.Entities;
using Core.Specifications;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.IServices;

public interface ITrackService : IGenericMultiFileService<Track, TrackDto>
{
    Task<TrackDto?> CreateWithAudioFileAsync(TrackCreateDto trackCreateDto, IFormFile file);

    Task<List<TrackDto>> GetByUserIdAsync(string userId);

    Task<IReadOnlyList<TrackCardDto>> GetTrackCardListByUserIdAsync(TrackSpecification specs);

    Task<TrackCardDto?> HandleTrackCardPostAsync(
        TrackCardDto? trackCardDto,
        IFormFile audioFile,
        IFormFile? visualFile
    );

    Task<TrackCardDto> GetTrackCardByTrackIdAsync(Guid id);

    Task<(FileStream?, string?)> GetPictureByTrackIdAsync(Guid id);

    Task<int> CountAsync(TrackSpecification spec);
}
