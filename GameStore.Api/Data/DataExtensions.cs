
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();
    }


    // Seeding Data
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connString = "Data Source=GameStore.db";

        builder.Services.AddSqlite<GameStoreContext>(
            connString,

                optionsAction: options => options.UseSeeding((context, _) =>
                {
                    if (!context.Set<Genre>().Any())
                    {
                        context.Set<Genre>().AddRange(
                            new Genre { Name = "Fighting" },
                            new Genre { Name = "Strategy" },
                            new Genre { Name = "RPG" },
                            new Genre { Name = "Action RPG" },
                            new Genre { Name = "Sandbox" },
                            new Genre { Name = "Battle Royale" },
                            new Genre { Name = "Shooter" },
                            new Genre { Name = "Simulation" },
                            new Genre { Name = "Adventure" },
                            new Genre { Name = "Sports" }
                        );
                        context.SaveChanges();
                    }
                })
            );
    }
}
