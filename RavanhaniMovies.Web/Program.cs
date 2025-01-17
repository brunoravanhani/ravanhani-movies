using RavanhaniMovies.Domain.Interfaces;
using RavanhaniMovies.Infra.Services;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeyedScoped<IMovieApi, MovieApi>("MovieApi");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/movie", async ([FromKeyedServices("MovieApi")] IMovieApi movieApi) =>
{
    var favoriteMovies = await movieApi.GetList("8507383");
    var myMovies = await movieApi.GetList("8507384");
    return favoriteMovies.ToList().Concat(myMovies).ToList();
})
.WithName("GetMovies")
.WithOpenApi();

app.Run();
