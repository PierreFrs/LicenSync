// Copyright : Pierre FRAISSE
// backend>backend>IContributionService.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.ContributionDTOs;
using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IContributionService
    : IGenericService<Contribution, ContributionDto>
{
    Task<Guid> GetContributionIdByLabelAsync(string label);
}
