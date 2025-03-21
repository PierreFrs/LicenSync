// <copyright file="ITrackRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Entities;

namespace Core.Interfaces.IRepositories;

public interface ITrackRepository : IGenericRepository<Track>
{
    Task<Track> StoreHashGuidInDatabaseAsync(Guid trackId, Guid hashId);
}
