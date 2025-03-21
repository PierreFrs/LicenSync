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
    IArtistRepository artistRepository,
    IMapper mapper
)
    : GenericService<Contribution, ContributionDto>(
        contributionRepository,
        mapper
    ),
        IContributionService
{
    private readonly IMapper mapper = mapper;

    public async Task<ContributionDto?> GetByArtistIdAsync(Guid id)
    {
        var artist = await artistRepository.GetByIdAsync(id);
        if (artist == null)
        {
            throw new ArgumentException($"No artist found with ID {id}", nameof(id));
        }

        var contributionId = artist.ContributionId;
        var contribution = await contributionRepository.GetByIdAsync(contributionId);

        if (contribution == null)
        {
            return null;
        }

        return mapper.Map<ContributionDto>(contribution);
    }

    public Task<Guid> GetContributionIdByLabelAsync(string label)
    {
        return contributionRepository.GetContributionIdByLabelAsync(label);
    }
}
