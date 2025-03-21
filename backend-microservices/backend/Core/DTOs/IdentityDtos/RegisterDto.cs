// RegisterDto.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 23/07/2024
// Fichier Modifié le : 23/07/2024
// Code développé pour le projet : backend

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.IdentityDtos;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
