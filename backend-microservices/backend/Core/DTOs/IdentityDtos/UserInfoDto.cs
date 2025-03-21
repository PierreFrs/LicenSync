// UserInfoDto.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 23/07/2024
// Fichier Modifié le : 23/07/2024
// Code développé pour le projet : backend

namespace Core.DTOs.IdentityDtos;

public class UserInfoDto
{
    public string? PhoneNumber { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? ProfilePicturePath { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }
}
