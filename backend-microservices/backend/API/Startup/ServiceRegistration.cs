// <copyright file="ServiceRegistration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.Interfaces.IHelpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Interfaces.IUnitOfWork;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.UnitOfWorks;
using Infrastructure.Helpers;
using Infrastructure.Services;

namespace API.Startup;

public static class ServiceRegistration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IFileValidationService, FileValidationService>();
        services.AddTransient<IFileHelpers, FileHelpers>();
        services.AddTransient<IHashingService, HashingService>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IAlbumService, AlbumService>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IContributionService, ContributionService>();
        services.AddScoped<IContributionRepository, ContributionRepository>();
        services.AddScoped<IArtistService, ArtistService>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<ITrackArtistContributionService, ITrackArtistContributionService>();
        services.AddScoped<ITrackArtistContributionRepository, TrackArtistContributionRepository>();
        services.AddScoped<IAlbumUploadUnitOfWork, AlbumUploadUnitOfWork>();
    }
}
