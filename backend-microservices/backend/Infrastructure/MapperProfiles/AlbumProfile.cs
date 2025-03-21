// Copyright : Pierre FRAISSE
// backend>backend>AlbumProfile.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.AlbumDTOs;
using Core.Entities;

namespace Infrastructure.MapperProfiles;

public class AlbumProfile : Profile
{
    public AlbumProfile()
    {
        CreateMap<Album, AlbumDto>().ReverseMap();
    }
}
