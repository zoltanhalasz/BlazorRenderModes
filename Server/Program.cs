using Server;
using Server.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration["Azure:SignalR:ConnectionString"]);
builder.Services.AddRazorComponents(options => options.DetailedErrors = true)
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

string tmdbKey = builder.Configuration["TMDBKey"];

builder.Services.AddScoped(sp => {
    var client = new HttpClient();
    client.BaseAddress = new("https://api.themoviedb.org/3/");
    client.DefaultRequestHeaders.Authorization = new("Bearer", tmdbKey);
    return client;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.MapGet("/movie/popular", async ([FromServices] HttpClient http) =>
{
    PopularMovieResponse? response = await http.GetFromJsonAsync<PopularMovieResponse>("movie/popular");

    return response is not null ? Results.Ok(response) : Results.Problem();
});

app.MapGet("/movie/{id}", async ([FromServices] HttpClient http, int? id) =>
{
    if (id.HasValue)
    {
        MovieDetails? response = await http.GetFromJsonAsync<MovieDetails?>($"movie/{id.Value}");

        return Results.Ok(response);
    }

    return Results.BadRequest();
});

app.Run();
