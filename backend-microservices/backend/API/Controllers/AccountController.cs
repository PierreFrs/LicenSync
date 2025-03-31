// AccountController.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 23/07/2024
// Fichier Modifié le : 23/07/2024
// Code développé pour le projet : backend

using AutoMapper;
using Core.DTOs.IdentityDtos;
using Core.Entities;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController(SignInManager<AppUser> signInManager, IMapper mapper)
    : BaseApiController
{
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AppUserDto>> Register([FromBody] RegisterDto registerDto)
    {
        var user = new AppUser
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            UserName = registerDto.Email,
        };

        var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            await signInManager.UserManager.AddToRoleAsync(user, "User");
            return Ok();
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await signInManager.PasswordSignInAsync(
            loginDto.Email,
            loginDto.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        if (result.Succeeded)
        {
            // Generate and return a JWT token or similar
            return Ok();
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Unauthorized(ModelState);
    }

    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<ActionResult<AppUserDto>> Logout()
    {
        await signInManager.SignOutAsync();

        return NoContent();
    }

    [HttpGet]
    [Route("user-info")]
    public async Task<ActionResult> GetUserInfo()
    {
        if (User.Identity?.IsAuthenticated == false)
        {
            return NoContent();
        }

        var user = await signInManager.UserManager.GetUserByEmailWithInfo(User);

        var userInfoDto = mapper.Map<UserInfoDto>(user.UserInfo);

        return Ok(
            new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                UserInfo = userInfoDto,
            }
        );
    }

    [HttpGet]
    [Route("auth-status")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public ActionResult GetAuthState()
    {
        return Ok(new { isAuthenticated = User.Identity?.IsAuthenticated ?? false });
    }

    [HttpPost]
    [Route("user-details")]
    public async Task<ActionResult<UserInfo>> CreateOrUpdateUserInfo(UserInfoDto userInfoDto)
    {
        var user = await signInManager.UserManager.GetUserByEmailWithInfo(User);

        if (user.UserInfo == null)
        {
            user.UserInfo = mapper.Map(userInfoDto, new UserInfo());
        }
        else
        {
            mapper.Map(userInfoDto, user.UserInfo);
        }

        var result = await signInManager.UserManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest("Problem updating user info");
        }

        return Ok(mapper.Map<UserInfoDto>(user.UserInfo));
    }
}
