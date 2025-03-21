// <copyright file="IContributionRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;

namespace Core.Interfaces.IRepositories;

public interface IContributionRepository : IGenericRepository<Contribution>
{
    Task<Guid> GetContributionIdByLabelAsync(string label);
}
