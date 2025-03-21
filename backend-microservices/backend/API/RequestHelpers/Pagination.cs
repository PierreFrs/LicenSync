// Pagination.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 23/09/2024
// Fichier Modifié le : 23/09/2024
// Code développé pour le projet : API

namespace API.RequestHelpers;

public class Pagination<T>(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
{
    public int PageIndex { get; set; } = pageIndex;

    public int PageSize { get; set; } = pageSize;

    public int Count { get; set; } = count;

    public IReadOnlyList<T> Data { get; set; } = data;
}
