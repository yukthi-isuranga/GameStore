using GameStore.Api.Dtos;

const string GetGameEndpointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games =
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

// GET /Games
// app.MapGet("/", () => "Hello World!");
app.MapGet("/games", () => games);



// GET /games/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);


// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
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
app.MapPut("/games/{id}", (int id, UpdateGameDto updateGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

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
app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});


app.Run();
