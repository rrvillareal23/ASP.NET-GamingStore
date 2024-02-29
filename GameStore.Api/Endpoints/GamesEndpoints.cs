using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        InMemGamesRepository repository = new();

        var group = routes.MapGroup("/games")
                        .WithParameterValidation();

        //get all games
        group.MapGet("/", () => repository.GetAll);

        //get game with specific Id
        group.MapGet("/{id}", (int id) =>
        {
            Game? game = repository.Get(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        })
        .WithName(GetGameEndPointName);

        //add game
        group.MapPost("/", (Game game) =>
        {
            repository.Create(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        //update game
        group.MapPut("/{id}", (int id, Game updatedGame) =>
        {
            Game? existingGame = repository.Get(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updatedGame.Name;
            existingGame.Genre = updatedGame.Genre;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
            existingGame.ImageUri = updatedGame.ImageUri;

            repository.Update(existingGame);

            return Results.NoContent();

        });

        //delete game
        group.MapDelete("/{id}", (int id) =>
        {
            Game? game = repository.Get(id);

            if (game is not null)
            {
                repository.Delete(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}