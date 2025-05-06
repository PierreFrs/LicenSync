// TrackSpecification.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 19/09/2024
// Fichier Modifié le : 19/09/2024
// Code développé pour le projet : Core

using Core.DTOs.TrackDTOs;
using Core.Entities;

namespace Core.Specifications;

public class TrackSpecification : BaseSpecification<Track, TrackCardDto>
{
    public TrackSpecification(string userId)
        : base(track => track.UserId == userId && track.ReleaseDate <= DateTime.Now) { }

    public TrackSpecification(Guid trackId)
        : base(track => track.Id == trackId && track.ReleaseDate <= DateTime.Now)
    {
        AddInclude(track => track.Album ?? new Album());
        AddInclude(track => track.FirstGenre ?? new Genre());
        AddInclude(track => track.SecondaryGenre ?? new Genre());
        AddInclude(track => track.ArtistContributions);
        ApplySelect();
    }

    public TrackSpecification(string userId, TrackSpecParams specParams)
        : base(track =>
            track.UserId == userId
            && (
                string.IsNullOrEmpty((specParams.Search))
                || track.TrackTitle.Contains(specParams.Search, StringComparison.CurrentCultureIgnoreCase)
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
                || specParams.ReleaseDates.Contains(track.ReleaseDate.ToString("yyyy-MM-dd"))
            )
            && track.ReleaseDate <= DateTime.Now
        )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        ApplySorting(specParams);

        // Add includes for related entities
        AddInclude(track => track.Album ?? new Album());
        AddInclude(track => track.FirstGenre ?? new Genre());
        AddInclude(track => track.SecondaryGenre ?? new Genre());
        AddInclude(track => track.ArtistContributions);
        AddInclude(track => track.ArtistContributions.Select(ac => ac.Artist));
        AddInclude(track => track.ArtistContributions.Select(ac => ac.Contribution));

        // Apply projection to TrackCardDto
        ApplySelect();
    }

    private void ApplySorting(TrackSpecParams specParams)
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
                AddOrderBy(track => track.TrackTitle);
                break;
            case "titleDesc":
                AddOrderByDescending(track => track.TrackTitle);
                break;
            default:
                AddOrderBy(track => track.ReleaseDate);
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
            ArtistsLyrics = track.ArtistContributions
                .Where(ac => ac.Contribution.Label == "Paroles")
                .Select(ac => $"{ac.Artist.Firstname} {ac.Artist.Lastname}")
                .ToList(),
            ArtistsMusic = track.ArtistContributions
                .Where(ac => ac.Contribution.Label == "Musique")
                .Select(ac => $"{ac.Artist.Firstname} {ac.Artist.Lastname}")
                .ToList(),
            ArtistsMusicAndLyrics = track.ArtistContributions
                .Where(ac => ac.Contribution.Label == "Musique et paroles")
                .Select(ac => $"{ac.Artist.Firstname} {ac.Artist.Lastname}")
                .ToList(),
            TrackAudioFilePath = track.AudioFilePath,
            ReleaseDate = track.ReleaseDate,
        });
    }
}
