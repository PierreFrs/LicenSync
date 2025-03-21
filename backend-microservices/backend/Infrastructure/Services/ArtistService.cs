// <copyright file="ArtistService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Core.DTOs.ArtistDTOs;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Specifications;

namespace Infrastructure.Services;

public class ArtistService(
    IArtistRepository artistRepository,
    ITrackRepository trackRepository,
    IMapper mapper
) : GenericService<Artist, ArtistDto>(artistRepository, mapper), IArtistService
{
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyList<ArtistDto?>?> GetArtistsByTrackIdAsync(Guid id)
    {
        var validTrack = await trackRepository.GetByIdAsync(id);
        if (validTrack == null)
        {
            return null;
        }
        var specs = new ArtistSpecification(id);
        var artists = await artistRepository.GetEntityListBySpecificationAsync(specs);
        return _mapper.Map<List<ArtistDto>>(artists) ?? new List<ArtistDto>();
    }
}
