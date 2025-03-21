// UserManagerExtensions.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 23/07/2024
// Fichier Modifié le : 23/07/2024
// Code développé pour le projet : backend

using System.Security.Authentication;
using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser> GetUserByEmail(
        this UserManager<AppUser> userManager,
        ClaimsPrincipal user
    )
    {
        var userToReturn =
            await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.GetEmail())
            ?? throw new AuthenticationException("User not found");

        return userToReturn;
    }

    public static async Task<AppUser> GetUserByEmailWithInfo(
        this UserManager<AppUser> userManager,
        ClaimsPrincipal user
    )
    {
        var userToReturn =
            await userManager
                .Users.Include(x => x.UserInfo)
                .FirstOrDefaultAsync(x => x.Email == user.GetEmail())
            ?? throw new AuthenticationException("User not found");

        return userToReturn;
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        var email =
            user.FindFirstValue(ClaimTypes.Email)
            ?? throw new AuthenticationException("Email not found");

        return email;
    }
}
