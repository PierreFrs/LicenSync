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
        : base(album => album.UserId == userId && album.ReleaseDate <= DateTime.Now) { }

    public AlbumSpecification(Guid albumId)
        : base(album => album.Id == albumId && album.ReleaseDate <= DateTime.Now)
    {
        AddInclude(album => album.Tracks ?? new List<Track>());
        ApplySelect();
    }

    public AlbumSpecification(string userId, AlbumSpecParams specParams)
        : base(album =>
            album.UserId == userId
            && (
                string.IsNullOrEmpty((specParams.Search))
                || album.AlbumTitle.ToLower().Contains(specParams.Search)
            )
            && (specParams.Titles.Count == 0 || specParams.Titles.Contains(album.AlbumTitle))
            && (
                specParams.ReleaseDates.Count == 0
                || specParams.ReleaseDates.Contains(album.ReleaseDate.ToString("yyyy-MM-dd"))
            )
            && album.ReleaseDate <= DateTime.Now
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
            case "releaseAsc":
                AddOrderBy(track => track.ReleaseDate);
                break;
            case "releaseDesc":
                AddOrderByDescending(track => track.ReleaseDate);
                break;
            case "titleAsc":
                AddOrderBy(track => track.AlbumTitle);
                break;
            case "titleDesc":
                AddOrderByDescending(track => track.AlbumTitle);
                break;
            default:
                AddOrderBy(track => track.ReleaseDate);
                break;
        }
    }

    private void ApplySelect()
    {
        AddSelect(album => new AlbumCardDto
        {
            AlbumTitle = album.AlbumTitle,
            AlbumVisualPath = album.AlbumVisualPath,
            ReleaseDate = album.ReleaseDate,
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
                    ReleaseDate = track.ReleaseDate,
                })
                .ToList(), // Convert tracks to TrackCardDto
        });
    }
}
