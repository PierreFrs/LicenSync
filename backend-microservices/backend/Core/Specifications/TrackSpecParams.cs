// TrackSpecParams.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 23/09/2024
// Fichier Modifié le : 23/09/2024
// Code développé pour le projet : Core

namespace Core.Specifications;

public class TrackSpecParams
{
    private const int MaxPageSize = 16;

    public int PageIndex { get; set; } = 1;

    private int _pageSize = 16;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    private List<string> _titles = [];
    public List<string> Titles
    {
        get => _titles;
        set
        {
            _titles = value
                .SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }
    }

    private List<string> _albums = [];
    public List<string> Albums
    {
        get => _albums;
        set
        {
            _albums = value
                .SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }
    }

    private List<string> _genres = [];
    public List<string> Genres
    {
        get => _genres;
        set
        {
            _genres = value
                .SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }
    }

    private List<string> _releaseDates = [];
    public List<string> ReleaseDates
    {
        get => _releaseDates;
        set
        {
            _releaseDates = value
                .SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }
    }

    public string? Sort { get; set; }

    private string? _search;
    public string? Search
    {
        get => _search ?? "";
        set => _search = value?.ToLower() ?? "";
    }
}
