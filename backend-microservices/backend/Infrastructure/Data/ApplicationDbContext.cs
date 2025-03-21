// Copyright : Pierre FRAISSE
// backend>backend>ApplicationDbContext.cs
// Created : 2024/05/1414 - 13:05

using System.Reflection;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
    public DbSet<UserInfo> UserInfos { get; set; }

    public DbSet<Track> Tracks { get; set; }

    public DbSet<Album> Albums { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Artist> Artists { get; set; }

    public DbSet<Contribution> Contributions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /********** CONFIGURATIONS **********/
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
