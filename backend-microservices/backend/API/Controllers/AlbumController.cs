// <copyright file="AlbumController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.DTOs.AlbumDTOs;
using Core.DTOs.CardDTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class AlbumController(
    IAlbumService albumService,
    IFileValidationService fileValidationService,
    UserManager<AppUser> userManager
) : BaseApiController
{
    /// POST
    /// <summary>
    /// Creates an object of type Album.
    /// </summary>
    /// <returns>Album.</returns>
    [SwaggerResponse(200, "Album created", typeof(AlbumDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpPost]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] AlbumDto albumDto, IFormFile? file)
    {
        if (file != null)
        {
            fileValidationService.ValidatePictureFile(file);
        }

        var album = await albumService.CreateWithFileAsync(albumDto, file);

        if (album == null)
        {
            throw new ArgumentException("Something went wrong with the Album creation");
        }

        return Ok(album);
    }

    /// GET
    /// <summary>
    /// Gets a list of objects of type Album.
    /// </summary>
    /// <returns>List of Albums.</returns>
    [SwaggerResponse(200, "Album list returned", typeof(List<AlbumDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpGet]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var albums = await albumService.GetListAsync();
        return Ok(albums);
    }

    /// GET by ID
    /// <summary>
    /// Gets an object of type Album from its Id.
    /// </summary>
    /// <returns>Album.</returns>
    [SwaggerResponse(200, "Album returned", typeof(AlbumDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Album not found")]
    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var album = await albumService.GetByIdAsync(id);

        if (album == null)
        {
            return NotFound();
        }

        return Ok(album);
    }

    /// GET Album list by UserId
    /// <summary>
    /// Gets a list of albums from a UserId.
    /// </summary>
    /// <returns>List of Albums.</returns>
    [SwaggerResponse(200, "Album list returned", typeof(List<AlbumDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Album not found")]
    [HttpGet]
    [Route("user/{userId:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetAlbumListByUserId([FromRoute] string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var albums = await albumService.GetAlbumListByUserIdAsync(userId);

        if (albums == null || albums.Count == 0)
        {
            return NoContent();
        }

        return Ok(albums);
    }

    /// GET
    /// <summary>
    /// Gets a list of objects of type AlbumCard.
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <returns>List of AlbumCards.</returns>
    [SwaggerResponse(200, "AlbumCard list returned", typeof(List<AlbumCardDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "User not found")]
    [HttpGet]
    [Route("album-card-list/{userId:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetAlbumCardListByUserId([FromRoute] string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var albumCards = await albumService.GetAlbumCardListByUserIdAsync(userId);

        return Ok(albumCards);
    }

    /// GET
    /// <summary>
    /// GET Album Card by Album ID
    /// </summary>
    /// <param name="albumId">Album ID</param>
    /// <returns>Album Card</returns>
    [SwaggerResponse(200, "Gets an Album Card by Album Id", typeof(AlbumCardDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Album Not Found")]
    [HttpGet]
    [Route("album-card/{albumId:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetAlbumCardByAlbumId([FromRoute] Guid albumId)
    {
        var album = await albumService.GetByIdAsync(albumId);
        if (album == null)
        {
            return NotFound();
        }

        var albumCard = await albumService.GetAlbumCardByAlbumIdAsync(albumId);
        return Ok(albumCard);
    }

    /// PUT
    /// <summary>
    /// Updates an object of type Album from its Id.
    /// </summary>
    /// Album Id
    /// <returns>Album.</returns>
    [SwaggerResponse(200, "Album updated", typeof(AlbumDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Album not found")]
    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromForm] AlbumDto albumDto,
        IFormFile? file
    )
    {
        if (file != null)
        {
            fileValidationService.ValidatePictureFile(file);
        }

        var album = await albumService.UpdateWithFileAsync(id, albumDto, file);

        if (album == null)
        {
            return NotFound();
        }

        return Ok(album);
    }

    /// DELETE
    /// <summary>
    /// Deletes an object of type Album from its Id.
    /// </summary>
    /// Album Id
    /// <returns>true.</returns>
    [SwaggerResponse(200, "Album deleted", typeof(AlbumDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Album not found")]
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var album = await albumService.GetByIdAsync(id);

        if (album == null)
        {
            return NotFound();
        }

        var isDeleted = await albumService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new ArgumentException("Something went wrong with the Album deletion");
        }

        return Ok(isDeleted);
    }
}
