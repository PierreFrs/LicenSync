// AlbumSpecParams.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 24/09/2024
// Fichier Modifié le : 24/09/2024
// Code développé pour le projet : Core

namespace Core.Specifications;

public class AlbumSpecParams
{
    private const int MaxPageSize = 20;

    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;
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
