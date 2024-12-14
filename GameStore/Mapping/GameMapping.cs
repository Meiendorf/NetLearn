using GameStore.Dtos;
using GameStore.Entitities;

namespace GameStore.Mapping;

public static class GameMapping
{
    public static Game toEntity(this CreateGameDto dto)
    {
        return new Game()
        {
            Name = dto.Name,
            Price = dto.Price,
            ReleaseDate = dto.ReleaseDate,
            GenreId = dto.GenreId,
        };
    }

    public static GameDto toDto(this Game game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }
    
    public static GameDetailsDto toDetailsDto(this Game game)
    {
        return new GameDetailsDto(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }
}