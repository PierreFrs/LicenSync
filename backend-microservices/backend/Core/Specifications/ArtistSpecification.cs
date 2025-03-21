// ArtistSpecification.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 19/09/2024
// Fichier Modifié le : 19/09/2024
// Code développé pour le projet : Core

using Core.Entities;

namespace Core.Specifications;

public class ArtistSpecification(Guid? trackId)
    : BaseSpecification<Artist>(x => x.TrackId == trackId);
