using ScreenSound.Shared.Data.DataBase;
using ScreenSound.Shared.Models.Models;
using ScreenSoundAPI.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddScoped<Repository<Artista>>();
builder.Services.AddScoped<Repository<Musica>>();
builder.Services.AddScoped<Repository<Genero>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();
app.AddEndpointsArtistas();
app.AddEndpointsMusicas();
app.AddEndpointsGeneros();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
