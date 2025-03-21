// Copyright : Pierre FRAISSE
// backend>backend>GenreProfile.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.GenreDTOs;
using Core.Entities;

namespace Infrastructure.MapperProfiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>().ReverseMap();
    }
}
