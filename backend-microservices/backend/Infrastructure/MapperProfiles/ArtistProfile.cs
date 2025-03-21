// Copyright : Pierre FRAISSE
// backend>backend>ArtistProfile.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.ArtistDTOs;
using Core.Entities;

namespace Infrastructure.MapperProfiles;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        CreateMap<Artist, ArtistDto>().ReverseMap();
    }
}
