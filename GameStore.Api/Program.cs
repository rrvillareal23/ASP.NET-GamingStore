using GameStore.Api.Entities;

List<Game> games = new()
{
    new Game()
    {
        Id=1,
        Name="Street Fighter II",
        Genre="Fighting",
        Price=19.99M,
        ReleaseDate=new DateTime(1991, 2, 1),
        ImageUri = "https://placehold.co/100"

    },
    new Game()
    {
        Id=2,
        Name="Final Fantasy XIV",
        Genre="Role Playing",
        Price=59.99M,
        ReleaseDate=new DateTime(2010, 9, 30),
        ImageUri = "https://placehold.co/100"

    }, 
    new Game()
    {
        Id=3,
        Name="FIFA 23",
        Genre="Sports",
        Price=69.99M,
        ReleaseDate=new DateTime(2022, 9, 27),
        ImageUri = "https://placehold.co/100"

    },

};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/games", () => games);

app.Run();
