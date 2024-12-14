using GameStore.Data;
using GameStore.Dtos;
using GameStore.Entitities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();

        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            return await dbContext.Games
                .Include(g => g.Genre)
                .Select(x => x.toDto())
                .AsNoTracking()
                .ToListAsync();
        });

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game.toDetailsDto());
        });

        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            var game = newGame.toEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            var dto = game.toDetailsDto();

            return Results.Created($"/{game.Id}", dto);
        });

        group.MapPut("/{id}", async (int id, CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            var gameEntity = await dbContext.Games.FindAsync(id);
            if (gameEntity == null)
            {
                return Results.NotFound();
            }

            gameEntity.Name = newGame.Name;
            gameEntity.GenreId = newGame.GenreId;
            gameEntity.Price = newGame.Price; // Example property update
            gameEntity.ReleaseDate = newGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}