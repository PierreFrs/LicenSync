// <copyright file="ContributionService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Core.DTOs.ContributionDTOs;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Infrastructure.Services;

public class ContributionService(
    IContributionRepository contributionRepository,
    IMapper mapper
)
    : GenericService<Contribution, ContributionDto>(
        contributionRepository,
        mapper
    ),
        IContributionService
{
    public Task<Guid> GetContributionIdByLabelAsync(string label)
    {
        return contributionRepository.GetContributionIdByLabelAsync(label);
    }
}
