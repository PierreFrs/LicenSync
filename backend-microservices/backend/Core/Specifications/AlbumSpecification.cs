// AlbumSpecification.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 19/09/2024
// Fichier Modifié le : 19/09/2024
// Code développé pour le projet : Core

using Core.DTOs.CardDTOs;
using Core.Entities;

namespace Core.Specifications;

public class AlbumSpecification : BaseSpecification<Album, AlbumCardDto>
{
    public AlbumSpecification(string? userId)
        : base(track => track.UserId == userId) { }

    public AlbumSpecification(Guid albumId)
        : base(track => track.Id == albumId)
    {
        AddInclude(track => track.Tracks ?? new List<Track>());
        ApplySelect();
    }

    public AlbumSpecification(string userId, AlbumSpecParams specParams)
        : base(track =>
            track.UserId == userId
            && (
                string.IsNullOrEmpty((specParams.Search))
                || track.AlbumTitle.ToLower().Contains(specParams.Search)
            )
            && (specParams.Titles.Count == 0 || specParams.Titles.Contains(track.AlbumTitle))
            && (
                specParams.ReleaseDates.Count == 0
                || specParams.ReleaseDates.Contains(track.CreationDate.ToString("yyyy-MM-dd"))
            )
        )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        ApplySorting(specParams);

        // Add includes for related entities
        AddInclude(track => track.Tracks ?? new List<Track>());

        // Apply projection to AlbumCardDto
        ApplySelect();
    }

    private void ApplySorting(AlbumSpecParams specParams)
    {
        switch (specParams.Sort)
        {
            case "titleAsc":
                AddOrderBy(track => track.AlbumTitle);
                break;
            case "titleDesc":
                AddOrderByDescending(track => track.AlbumTitle);
                break;
            default:
                AddOrderBy(track => track.AlbumTitle);
                break;
        }
    }

    private void ApplySelect()
    {
        AddSelect(album => new AlbumCardDto
        {
            AlbumTitle = album.AlbumTitle,
            AlbumVisualPath = album.AlbumVisualPath,
            TrackCards = album
                .Tracks.Select(track => new TrackCardDto
                {
                    Id = track.Id,
                    TrackTitle = track.TrackTitle,
                    Length = track.Length,
                    RecordLabel = track.RecordLabel,
                    FirstGenre = track.FirstGenre != null ? track.FirstGenre.Label : null,
                    SecondaryGenre =
                        track.SecondaryGenre != null ? track.SecondaryGenre.Label : null,
                    AlbumTitle = album.AlbumTitle, // You already have the album info
                    BlockchainHash = track.BlockchainHashId,
                    ArtistsLyrics = track
                        .Artists.Where(a => a.Contribution.Label == "Paroles")
                        .Select(a => $"{a.Firstname} {a.Lastname}")
                        .ToList(),
                    ArtistsMusic = track
                        .Artists.Where(a => a.Contribution.Label == "Musique")
                        .Select(a => $"{a.Firstname} {a.Lastname}")
                        .ToList(),
                    ArtistsMusicAndLyrics = track
                        .Artists.Where(a => a.Contribution.Label == "Musique et paroles")
                        .Select(a => $"{a.Firstname} {a.Lastname}")
                        .ToList(),
                    TrackAudioFilePath = track.AudioFilePath,
                    TrackVisualFilePath = track.TrackVisualPath,
                })
                .ToList(), // Convert tracks to TrackCardDto
        });
    }
}
