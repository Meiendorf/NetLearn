using GameStore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDto> games =
[
    new(1, "Street Fighter", "Fighting", 100.00M, new DateOnly(1992, 7, 15)),
    new(2, "Final Fanstasy", "Roleplaying", 102.00M, new DateOnly(2010, 7, 15)),
    new(3, "FIFA 2023", "Sports", 102.00M, new DateOnly(2023, 7, 15)),
];

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) => { return games.Find(g => g.Id == id); });

app.MapPost("/games", (CreateGameDto newGame) =>
{
    var game = new GameDto(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return game;
});

app.MapPut("/games/{id}", (int id, CreateGameDto newGame) =>
{
    var index = games.FindIndex(game => game.Id == id);
    var game = new GameDto(
        id,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games[index] = game;

    return Results.NoContent();
});

app.MapGet("/", () => { return games; });

app.Run();