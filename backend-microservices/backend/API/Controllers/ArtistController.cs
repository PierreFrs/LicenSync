// Copyright : Pierre FRAISSE
// backend>backend>ArtistController.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.ArtistDTOs;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class ArtistController(IArtistService artistService, ITrackService trackService)
    : BaseApiController
{
    /// POST
    /// <summary>
    /// Creates an object of type Artist
    /// </summary>
    /// <returns>Artist</returns>
    [SwaggerResponse(200, "Creates an object of type Artist", typeof(ArtistDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] ArtistDto artistDto)
    {
        var artist = await artistService.CreateAsync(artistDto);

        if (artist == null)
        {
            throw new ArgumentException("Something went wrong with the Artist creation");
        }

        return Ok(artist);
    }

    /// GET
    /// <summary>
    /// Gets a list of objects of type Artist
    /// </summary>
    /// <returns>List of Artists</returns>
    [SwaggerResponse(200, "Gets a list of objects of type Artist", typeof(List<ArtistDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var artists = await artistService.GetListAsync();
        return Ok(artists);
    }

    /// GET by ID
    /// <summary>
    /// Gets an object of type Artist from its Id
    /// </summary>
    /// <returns>Artist</returns>
    [SwaggerResponse(200, "Gets an object of type Artist by Id", typeof(ArtistDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var artist = await artistService.GetByIdAsync(id);

        if (artist == null)
        {
            return NotFound();
        }

        return Ok(artist);
    }

    /// GET Artist by Track ID
    /// <summary>
    /// Gets a list of Artists from a trackId
    /// </summary>
    /// <returns>List of Artists</returns>
    [SwaggerResponse(200, "Gets a list of Artists from a trackId", typeof(List<ArtistDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpGet]
    [Route("track/{trackId:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetArtistsByTrackId([FromRoute] Guid trackId)
    {
        var track = await trackService.GetByIdAsync(trackId);
        if (track == null)
        {
            return NotFound();
        }

        var artists = await artistService.GetArtistsByTrackIdAsync(trackId);

        if (artists == null || artists.Count == 0)
        {
            return NotFound();
        }

        return Ok(artists);
    }

    /// PUT
    /// <summary>
    /// Updates an object of type Artist from its Id
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>Artist</returns>
    [SwaggerResponse(200, "Updates an object of type Artist", typeof(ArtistDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromForm] ArtistDto artistDto
    )
    {
        var artist = await artistService.GetByIdAsync(id);
        if (artist == null)
        {
            return NotFound();
        }

        var updatedArtist = await artistService.UpdateAsync(id, artistDto);
        if (updatedArtist == null)
        {
            throw new ArgumentException("Something went wrong with the update of the artist");
        }

        return Ok(updatedArtist);
    }

    /// DELETE
    /// <summary>
    /// Deletes an object of type Artist from its Id
    /// </summary>
    /// Artist ID
    /// <returns>true</returns>
    [SwaggerResponse(200, "Deletes an object of type Artist")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var artist = await artistService.GetByIdAsync(id);

        if (artist == null)
        {
            return NotFound();
        }

        var isDeleted = await artistService.DeleteAsync(id);

        if (!isDeleted)
        {
            throw new ArgumentException("Something went wrong with the deletion of the artist");
        }

        return Ok(isDeleted);
    }
}
