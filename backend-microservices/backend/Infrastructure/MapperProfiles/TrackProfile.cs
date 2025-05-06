// Copyright : Pierre FRAISSE
// backend>backend>TrackProfile.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.TrackDTOs;
using Core.Entities;
using Core.Interfaces.IServices;

namespace Infrastructure.MapperProfiles;

public class TrackProfile : Profile
{
    public TrackProfile()
    {
        CreateMap<Track, TrackDto>().ReverseMap();
        CreateMap<Track, TrackCreateDto>().ReverseMap();
        CreateMap<TrackDto, TrackCardDto>()
            .ForMember(dest => dest.TrackTitle, opt => opt.MapFrom(src => src.TrackTitle))
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))
            .ForMember(dest => dest.RecordLabel, opt => opt.MapFrom(src => src.RecordLabel))
            .ForMember(dest => dest.BlockchainHash, opt => opt.MapFrom(src => src.BlockchainHashId))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(
                dest => dest.TrackAudioFilePath,
                opt => opt.MapFrom(src => src.AudioFilePath)
            )
            // Ignore the properties that will be set manually
            .ForMember(dest => dest.FirstGenre, opt => opt.Ignore())
            .ForMember(dest => dest.SecondaryGenre, opt => opt.Ignore())
            .ForMember(dest => dest.AlbumTitle, opt => opt.Ignore())
            .ReverseMap();
    }
}
