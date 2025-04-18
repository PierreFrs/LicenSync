// <copyright file="DatabaseMigration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>


using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Startup;

public static class DatabaseMigration
{
    public static async Task MigrateDatabaseAsync(this IServiceScope scope, IWebHostEnvironment env)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            if (env.IsEnvironment("test") || env.IsDevelopment())
            {
                logger.LogInformation("Applying database migrations...");
                await dbContext.Database.MigrateAsync();

                // Seed the database
                logger.LogInformation(
                    "Database migrations applied successfully. Now seeding the database..."
                );
                await AppContextSeed.SeedAsync(dbContext);
            }
            else
            {
                logger.LogInformation("Environment is Production. Applying database migrations...");
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Seeding the database...");
                await AppContextSeed.SeedAsync(dbContext);
                logger.LogInformation("Database seeding completed.");

            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
        }
    }
}
