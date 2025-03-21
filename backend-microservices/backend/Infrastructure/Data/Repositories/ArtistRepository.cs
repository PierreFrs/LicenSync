// Copyright : Pierre FRAISSE
// backend>backend>ArtistRepository.cs
// Created : 2024/05/1414 - 13:05

using Core.Entities;
using Core.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class ArtistRepository(ApplicationDbContext context)
    : GenericRepository<Artist>(context),
        IArtistRepository;
