// <copyright file="IGenreRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;

namespace Core.Interfaces.IRepositories;

public interface IGenreRepository : IGenericRepository<Genre>
{
    Task<Guid> GetGenreIdByLabelAsync(string label);
}
