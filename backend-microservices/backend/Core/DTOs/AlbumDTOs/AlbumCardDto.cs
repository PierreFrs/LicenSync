// AlbumCardDto.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 20/09/2024
// Fichier Modifié le : 20/09/2024
// Code développé pour le projet : Core

using Core.DTOs.TrackDTOs;
using Core.Entities;

namespace Core.DTOs.AlbumDTOs;

public class AlbumCardDto : BaseDto
{
    public string AlbumTitle { get; set; } = string.Empty;

    public string RecordLabel { get; set; } = string.Empty;

    public Genre? FirstGenre { get; set; }

    public Genre? SecondaryGenre { get; set; }

    public string? AlbumVisualPath { get; set; }

    public DateTime ReleaseDate { get; set; }

    public IList<TrackCardDto> TrackCards { get; set; } = new List<TrackCardDto>();
}
