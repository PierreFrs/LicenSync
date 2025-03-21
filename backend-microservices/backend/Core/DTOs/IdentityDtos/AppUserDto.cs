// AppUserDto.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 23/07/2024
// Fichier Modifié le : 23/07/2024
// Code développé pour le projet : backend

namespace Core.DTOs.IdentityDtos;

public class AppUserDto
{
    public string? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}
