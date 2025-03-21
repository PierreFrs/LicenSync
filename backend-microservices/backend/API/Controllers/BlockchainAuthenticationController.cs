// <copyright file="BlockchainAuthenticationController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.DTOs.GenreDTOs;
using Core.DTOs.TrackDTOs;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class BlockchainAuthenticationController(
    IBlockchainAuthenticationService<TrackDto> blockchainAuthenticationService
) : BaseApiController
{
    /// POST
    /// <summary>
    /// Deployes the contract on the blockchain
    /// </summary>
    /// <returns>string</returns>
    [SwaggerResponse(200, "Contract deployed", typeof(GenreDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpPost]
    [Route("deploy-contract")]
    [Authorize]
    public async Task<IActionResult> DeployContractAsync()
    {
        var contractAddress = await blockchainAuthenticationService.DeployContractAsync();

        if (contractAddress == null)
        {
            return NotFound();
        }

        return this.Ok(contractAddress);
    }

    /// PUT
    /// <summary>
    /// Updates a track with it's authentication hash id and stores the hash and id on the blockchain
    /// </summary>
    /// <param name="trackId">Track Id<param>
    /// <returns>Track</returns>
    [SwaggerResponse(
        200,
        "Track authenticated successfully",
        typeof(TrackWithReceiptDto<TrackDto>)
    )]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpPut]
    [Route("store-hash/{trackId:guid}")]
    [Authorize]
    public async Task<IActionResult> AuthenticateTrackAsync(Guid trackId)
    {
        var authenticatedTrack = await blockchainAuthenticationService.AuthenticateTrackAsync(
            trackId
        );

        if (authenticatedTrack == null)
        {
            return NotFound();
        }

        return Ok(authenticatedTrack);
    }

    /// GET
    /// <summary>
    /// Gets the track hash id, retrieves the hash on the blockchain, hash the track again and compare the two hashes
    /// </summary>
    /// <returns>Track</returns>
    [SwaggerResponse(200, "Hash compared successfully", typeof(GenreDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpGet]
    [Route("compareHashes/{trackId:guid}")]
    [Authorize]
    public async Task<IActionResult> CompareTrackHashes(Guid trackId)
    {
        var dbHash = await blockchainAuthenticationService.GenerateNewTrackHashAsync(trackId);
        var blockchainHash = await blockchainAuthenticationService.GetTrackHashFromBlockchainAsync(
            trackId
        );

        if (dbHash == null)
        {
            return NotFound();
        }

        if (blockchainHash == null)
        {
            return NotFound();
        }

        return Ok(new { DatabaseHash = dbHash, BlockchainHash = blockchainHash });
    }
}
