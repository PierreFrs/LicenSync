// AlbumCardDto.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 20/09/2024
// Fichier Modifié le : 20/09/2024
// Code développé pour le projet : Core

namespace Core.DTOs.CardDTOs;

public class AlbumCardDto : BaseDto
{
    public string AlbumTitle { get; set; } = String.Empty;

    public string? AlbumVisualPath { get; set; }

    public DateTime ReleaseDate { get; set; }

    public IList<TrackCardDto> TrackCards { get; set; } = new List<TrackCardDto>();
}
