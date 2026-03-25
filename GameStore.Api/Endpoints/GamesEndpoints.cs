
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games =
[
    new(1,  "League of Legends",   "Strategy",  19.99M, new DateOnly(2009, 10, 27)),
    new(2,  "Minecraft",           "Sandbox",   26.99M, new DateOnly(2011,  11, 18)),
    new(3,  "Cyberpunk 2077",      "RPG",       59.99M, new DateOnly(2020, 12, 10)),
    new(4,  "Fortnite",            "Battle Royale", 0.00M, new DateOnly(2017, 7, 25)),
    new(5,  "The Witcher 3",       "RPG",       39.99M, new DateOnly(2015,  5, 19)),
    new(6,  "Valorant",            "Shooter",    0.00M, new DateOnly(2020,  6,  2)),
    new(7,  "Elden Ring",          "Action RPG", 59.99M, new DateOnly(2022,  2, 25)),
    new(8,  "Stardew Valley",      "Simulation", 14.99M, new DateOnly(2016,  2, 26)),
    new(9,  "Apex Legends",        "Battle Royale", 0.00M, new DateOnly(2019, 2,  4)),
    new(10, "Red Dead Redemption 2","Adventure", 59.99M, new DateOnly(2018, 10, 26)),
];

    public static void MapGameEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");

        // GET /Games
        // app.MapGet("/", () => "Hello World!");
        group.MapGet("/", () => games);



        // GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            var data = games.Find(game => game.Id == id);

            return data is null ? Results.NotFound() : Results.Ok(data);

        }).WithName(GetGameEndpointName);


        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        // PUT /game/1
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );

            return Results.NoContent();
        });


        // DELETE /games/3
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

    }
}
