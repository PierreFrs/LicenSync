// Copyright : Pierre FRAISSE
// backend>backend>TrackController.cs
// Created : 2024/05/1717 - 08:05

using Core.DTOs.TrackDTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IServices;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

public class TrackController(
    ITrackService trackService,
    IFileValidationService fileValidationService,
    UserManager<AppUser> userManager
) : BaseApiController
{
    /// POST
    /// <summary>
    /// Creates an object of type Track.
    /// </summary>
    /// <returns>Track</returns>
    [SwaggerResponse(200, "Creates an object of type Track", typeof(TrackDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(
        [FromForm] TrackCreateDto trackCreateDto,
        IFormFile audioFile
    )
    {
        if (audioFile == null)
        {
            throw new ArgumentException("Audio file is required and cannot be null.");
        }

        fileValidationService.ValidateAudioFile(audioFile);

        var track = await trackService.CreateWithAudioFileAsync(trackCreateDto, audioFile);

        if (track == null)
        {
            throw new ArgumentException("Something went wrong with the Track creation");
        }

        return Ok(track);
    }

    /// POST
    /// <summary>
    /// Handle track infos and records from a track card object
    /// </summary>
    /// <returns>TrackCard</returns>
    [SwaggerResponse(
        200,
        "Handle track infos and records from a track card object",
        typeof(TrackCardDto)
    )]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpPost]
    [Route("track-card")]
    [Authorize]
    public async Task<IActionResult> HandleTrackCard(
        [FromForm] TrackCardDto trackCardDto,
        IFormFile audioFile,
        IFormFile? visualFile
    )
    {
        try
        {
            var returnTrack = await trackService.HandleTrackCardPostAsync(
                trackCardDto,
                audioFile,
                visualFile
            );
            return Ok(returnTrack);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// GET
    /// <summary>
    /// Gets a list of objects of type Track.
    /// </summary>
    /// <returns>List of Tracks.</returns>
    [SwaggerResponse(200, "Gets a list of objects of type Track", typeof(List<TrackDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var tracks = await trackService.GetListAsync();
        return Ok(tracks);
    }

    /// GET by ID
    /// <summary>
    /// Gets an object of type Track from its ID.
    /// </summary>
    /// <returns>Track.</returns>
    [SwaggerResponse(200, "Gets an object of type Track by Id", typeof(TrackDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var track = await trackService.GetByIdAsync(id);

        if (track == null)
        {
            return NotFound();
        }

        return Ok(track);
    }

    /// GET by User ID
    /// <summary>
    /// Gets list of Tracks from a User ID
    /// </summary>
    /// <returns>List of Track</returns>
    [SwaggerResponse(200, "Gets list of Tracks from a User Id", typeof(List<TrackDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpGet]
    [Route("user/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var tracks = await trackService.GetByUserIdAsync(userId);

        return Ok(tracks);
    }

    /// GET track picture by Track ID
    /// <summary>
    /// Gets a picture file from a Track ID
    /// </summary>
    /// <returns>List of Track</returns>
    [SwaggerResponse(200, "Gets a picture file from a Track Id")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpGet]
    [Route("picture/{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetPictureByTrackId(Guid id)
    {
        var (fileStream, filename) = await trackService.GetPictureByTrackIdAsync(id);

        if (fileStream == null || filename == null)
        {
            return NoContent();
        }

        var fileExtension = Path.GetExtension(filename).ToLowerInvariant();

        var mimeType = fileExtension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            _ => throw new InvalidOperationException(
                $"Unsupported file extension: {fileExtension}"
            ),
        };

        return File(fileStream, mimeType, Path.GetFileName(filename));
    }

    /// GET
    /// <summary>
    /// GET Track Card list by user ID
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="specParams">Specification Parameters</param>
    /// <returns>Track Card List</returns>
    [SwaggerResponse(200, "Gets a Track Card by Track Id", typeof(TrackCardDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "User Not Found")]
    [HttpGet]
    [Route("track-card-list/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetTrackCardsByUserId(
        [FromRoute] string userId,
        [FromQuery] TrackSpecParams specParams
    )
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var spec = new TrackSpecification(userId, specParams);

        return await CreatePagedResult(
            trackService,
            spec,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    /// GET
    /// <summary>
    /// GET Track Card by Track ID
    /// </summary>
    /// <param name="trackId">Track ID</param>
    /// <returns>Track Card</returns>
    [SwaggerResponse(200, "Gets a Track Card by Track Id", typeof(TrackCardDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Track Not Found")]
    [HttpGet]
    [Route("track-card/{trackId:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetTrackCardByTrackId([FromRoute] Guid trackId)
    {
        try
        {
            var track = await trackService.GetByIdAsync(trackId);
            if (track == null)
            {
                throw new ArgumentException($"No track found with ID {trackId}");
            }

            var trackCard = await trackService.GetTrackCardByTrackIdAsync(trackId);
            return Ok(trackCard);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// PUT
    /// <summary>
    /// Updates an object of type Track from its ID
    /// </summary>
    /// <param name="id">Track ID</param>
    /// <param name="trackDtoUpdate">TracDtoUpdate</param>
    /// <param name="audioFile">IFormFile?</param>
    /// <returns>true</returns>
    [SwaggerResponse(200, "Updates an object of type Track")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromForm] TrackDto trackDto
    )
    {
        var track = await trackService.GetByIdAsync(id);
        if (track == null)
        {
            return NotFound();
        }

        var isUpdated = await trackService.UpdateAsync(id, trackDto);
        if (isUpdated == null)
        {
            throw new ArgumentException("Something went wrong with the update of the track");
        }

        return Ok(isUpdated);
    }

    /// DELETE
    /// <summary>
    /// Deletes an object of type Track from its ID
    /// </summary>
    /// <param name="id">Track ID</param>
    /// <returns>true</returns>
    [SwaggerResponse(200, "Deletes an object of type Track")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Not Found")]
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var track = await trackService.GetByIdAsync(id);

        if (track == null)
        {
            return NotFound();
        }

        var isDeleted = await trackService.DeleteAsync(id);
        if (!isDeleted)
        {
            throw new ArgumentException("Something went wrong with the deletion of the track");
        }

        return Ok(isDeleted);
    }
}
