// TrackSpecification.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 19/09/2024
// Fichier Modifié le : 19/09/2024
// Code développé pour le projet : Core

using Core.DTOs.CardDTOs;
using Core.Entities;

namespace Core.Specifications;

public class TrackSpecification : BaseSpecification<Track, TrackCardDto>
{
    public TrackSpecification(string userId)
        : base(track => track.UserId == userId) { }

    public TrackSpecification(Guid trackId)
        : base(track => track.Id == trackId)
    {
        AddInclude(track => track.Album ?? new Album());
        AddInclude(track => track.FirstGenre ?? new Genre());
        AddInclude(track => track.SecondaryGenre ?? new Genre());
        ApplySelect();
    }

    public TrackSpecification(string userId, TrackSpecParams specParams)
        : base(track =>
            track.UserId == userId
            && (
                string.IsNullOrEmpty((specParams.Search))
                || track.TrackTitle.ToLower().Contains(specParams.Search)
            )
            && (specParams.Titles.Count == 0 || specParams.Titles.Contains(track.TrackTitle))
            && track.Album != null
            && (specParams.Albums.Count == 0 || specParams.Albums.Contains(track.Album.AlbumTitle))
            && track.FirstGenre != null
            && (
                specParams.Genres.Count == 0
                || specParams.Genres.Contains(track.FirstGenre.Label)
                || (track.SecondaryGenre != null && specParams.Genres.Contains(track.SecondaryGenre.Label))
            )
            && (
                specParams.ReleaseDates.Count == 0
                || specParams.ReleaseDates.Contains(track.CreationDate.ToString("yyyy-MM-dd"))
            )
        )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        ApplySorting(specParams);

        // Add includes for related entities
        AddInclude(track => track.Album ?? new Album());
        AddInclude(track => track.FirstGenre ?? new Genre());
        AddInclude(track => track.SecondaryGenre ?? new Genre());

        // Apply projection to TrackCardDto
        ApplySelect();
    }

    private void ApplySorting(TrackSpecParams specParams)
    {
        switch (specParams.Sort)
        {
            case "releaseAsc":
                AddOrderBy(track => track.CreationDate);
                break;
            case "releaseDesc":
                AddOrderByDescending(track => track.CreationDate);
                break;
            case "titleAsc":
                AddOrderBy(track => track.TrackTitle);
                break;
            case "titleDesc":
                AddOrderByDescending(track => track.TrackTitle);
                break;
            default:
                AddOrderBy(track => track.CreationDate);
                break;
        }
    }

    private void ApplySelect()
    {
        AddSelect(track => new TrackCardDto
        {
            Id = track.Id,
            TrackTitle = track.TrackTitle,
            Length = track.Length,
            RecordLabel = track.RecordLabel,
            FirstGenre = track.FirstGenre != null ? track.FirstGenre.Label : null,
            SecondaryGenre = track.SecondaryGenre != null ? track.SecondaryGenre.Label : null,
            AlbumTitle = track.Album != null ? track.Album.AlbumTitle : null,
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
        });
    }
}
