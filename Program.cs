using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string GetGameEndPointName = "GetGame";

List<GameDto> games = [
    new GameDto(1, "Elder Quest", "RPG", 59.99m, new DateOnly(2022, 5, 15)),
    new GameDto(2, "Speed Racers", "Racing", 39.99m, new DateOnly(2021, 11, 3)),
    new GameDto(3, "Battle Arena X", "Action", 49.99m, new DateOnly(2023, 2, 25)),
    new GameDto(4, "Farm Life", "Simulation", 19.99m, new DateOnly(2020, 7, 10))
];
// GET/GAMES
app.MapGet("games", () => games);

// Get/games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(GetGameEndPointName);

// CREATE NEW
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
});

// UPDATE NEW

app.MapPut("games/{id}", (int id, UpdateGameDto updateGame) =>
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

// Delete

app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();

});



app.Run();
