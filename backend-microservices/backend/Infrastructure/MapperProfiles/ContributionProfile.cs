// Copyright : Pierre FRAISSE
// backend>backend>ContributionProfile.cs
// Created : 2024/05/1414 - 13:05

using AutoMapper;
using Core.DTOs.ContributionDTOs;
using Core.Entities;

namespace Infrastructure.MapperProfiles;

public class ContributionProfile : Profile
{
    public ContributionProfile()
    {
        CreateMap<Contribution, ContributionDto>().ReverseMap();
    }
}
