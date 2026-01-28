using Backend.HttpClients;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Backend.Middlewares;
using Backend.Data;
using Backend.Repositories;


var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpClient PokeAPI
builder.Services.AddHttpClient<PokeApiClient>(client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
});

// Servicios
builder.Services.AddScoped<PokemonService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        "server=localhost;database=pokedb;user=root;password=admin",
        ServerVersion.AutoDetect("server=localhost;database=pokedb;user=root;password=admin")
    );
});

builder.Services.AddScoped<PokemonRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
