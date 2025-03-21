// Copyright : Pierre FRAISSE
// backend>backend>ContributionController.cs
// Created : 2024/05/1414 - 13:05


using Core.DTOs.ContributionDTOs;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class ContributionController(IContributionService contributionService) : BaseApiController
{
    /// POST
    /// <summary>
    /// Creates an object of type Contribution
    /// </summary>
    /// <returns>Contribution</returns>
    [SwaggerResponse(200, "Creates an object of type Contribution", typeof(ContributionDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] ContributionDto contributionDto)
    {
        var contribution = await contributionService.CreateAsync(contributionDto);

        if (contribution == null)
        {
            throw new ArgumentException("Something went wrong with the contribution creation");
        }

        return Ok(contribution);
    }

    /// GET
    /// <summary>
    /// Gets a list of objects of type Contribution
    /// </summary>
    /// <returns>List of Contributions</returns>
    [SwaggerResponse(
        200,
        "Gets a list of objects of type Contribution",
        typeof(List<ContributionDto>)
    )]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var contributions = await contributionService.GetListAsync();
        return Ok(contributions);
    }

    /// GET by ID
    /// <summary>
    /// Gets an object of type Contribution from its Id
    /// </summary>
    /// <returns>Contribution</returns>
    [SwaggerResponse(200, "Gets an object of type Contribution by Id", typeof(ContributionDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var contribution = await contributionService.GetByIdAsync(id);
        if (contribution == null)
        {
            return NotFound();
        }

        return Ok(contribution);
    }

    /// GET Contribution by Artist ID
    /// <summary>
    /// Gets the Contribution associated with an artist
    /// </summary>
    /// <returns>Contribution</returns>
    [SwaggerResponse(
        200,
        "Gets the Contribution associated with an artist",
        typeof(ContributionDto)
    )]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpGet]
    [Route("artist/{artistId:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetByArtistId([FromRoute] Guid artistId)
    {
        var contribution = await contributionService.GetByArtistIdAsync(artistId);

        if (contribution == null)
        {
            return NotFound();
        }

        return Ok(contribution);
    }

    /// PUT
    /// <summary>
    /// Updates an object of type Contribution from its Id
    /// </summary>
    /// <param name="id">Contribution Id<param>
    /// <returns>Contribution</returns>
    [SwaggerResponse(200, "Updates an object of type Contribution", typeof(ContributionDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromForm] ContributionDto contributionDto
    )
    {
        var contribution = await contributionService.UpdateAsync(id, contributionDto);

        if (contribution == null)
        {
            return NotFound();
        }

        return Ok(contribution);
    }

    /// DELETE
    /// <summary>
    /// Deletes an object of type Contribution from its Id
    /// </summary>
    /// <param name="id">Contribution Id<param>
    /// <returns>true</returns>
    [SwaggerResponse(200, "Deletes an object of type Contribution")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var contribution = await contributionService.GetByIdAsync(id);

        if (contribution == null)
        {
            return NotFound();
        }

        var isDeleted = await contributionService.DeleteAsync(id);

        if (!isDeleted)
        {
            throw new ArgumentException("Something went wrong with the contribution deletion");
        }

        return Ok(contribution);
    }
}
