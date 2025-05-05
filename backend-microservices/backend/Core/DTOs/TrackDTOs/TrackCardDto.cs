// TrackCardDto.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 19/09/2024
// Fichier Modifié le : 19/09/2024
// Code développé pour le projet : Core

namespace Core.DTOs.TrackDTOs;

public class TrackCardDto : BaseDto
{
    public string TrackTitle { get; set; } = string.Empty;

    public string Length { get; set; } = string.Empty;

    public string? RecordLabel { get; set; }

    public string? FirstGenre { get; set; }

    public string? SecondaryGenre { get; set; }

    public string? AlbumTitle { get; set; }

    public Guid? BlockchainHash { get; set; }

    public IList<string>? ArtistsLyrics { get; set; }

    public IList<string>? ArtistsMusic { get; set; }

    public IList<string>? ArtistsMusicAndLyrics { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public DateTime ReleaseDate { get; set; } = DateTime.Now;

    public string TrackAudioFilePath { get; set; } = string.Empty;

    public string? TrackVisualFilePath { get; set; }

    public string? UserId { get; set; } = string.Empty;
}
