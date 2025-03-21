// <copyright file="ContributionRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;
using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ContributionRepository(ApplicationDbContext context)
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    : GenericRepository<Contribution>(context),
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
        IContributionRepository
{
    public async Task<Guid> GetContributionIdByLabelAsync(string label)
    {
        var contribution = await context.Contributions.FirstOrDefaultAsync(c => c.Label == label);
        if (contribution == null)
        {
            throw new ArgumentException($"No contribution found with label {label}");
        }

        return contribution.Id;
    }
}
