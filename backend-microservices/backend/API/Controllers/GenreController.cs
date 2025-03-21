// Copyright : Pierre FRAISSE
// backend>backend>GenreController.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.GenreDTOs;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class GenreController(IGenreService genreService) : BaseApiController
{
    /// POST
    /// <summary>
    /// Creates an object of type Genre.
    /// </summary>
    /// <returns>Genre.</returns>
    [SwaggerResponse(200, "Genre created", typeof(GenreDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpPost]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] GenreDto genreDto)
    {
        var genre = await genreService.CreateAsync(genreDto);
        if (genre == null)
        {
            throw new ArgumentException("Something went wrong with the Genre creation");
        }

        return Ok(genre);
    }

    /// GET
    /// <summary>
    /// Gets a list of objects of type Genre
    /// </summary>
    /// <returns>List of Genres</returns>
    [SwaggerResponse(200, "Genre list returned", typeof(List<GenreDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpGet]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var genres = await genreService.GetListAsync();
        return Ok(genres);
    }

    /// GET by ID
    /// <summary>
    /// Gets an object of type Genre from its Id
    /// </summary>
    /// <returns>Genre</returns>
    [SwaggerResponse(200, "Genre returned", typeof(GenreDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Genre not found")]
    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var genre = await genreService.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    /// PUT
    /// <summary>
    /// Updates an object of type Genre from its Id
    /// </summary>
    /// Genre Id
    /// <returns>Genre</returns>
    [SwaggerResponse(200, "Genre updated", typeof(GenreDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Genre not found")]
    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromForm] GenreDto genreDto
    )
    {
        var genre = await genreService.UpdateAsync(id, genreDto);
        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    /// DELETE
    /// <summary>
    /// Deletes an object of type Genre from its Id
    /// </summary>
    /// Genre Id
    /// <returns>true</returns>
    [SwaggerResponse(200, "Genre deleted", typeof(GenreDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Genre not found")]
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var genre = await genreService.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        var isDeleted = await genreService.DeleteAsync(id);

        if (!isDeleted)
        {
            throw new ArgumentException("Something went wrong with the genre deletion");
        }

        return Ok(genre);
    }
}
