using ScreenSound.DataBase;
using ScreenSound.Modelos;
using ScreenSoundAPI.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<Repository<Artista>>();
builder.Services.AddTransient<Repository<Musica>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();
app.AddEndpointsArtistas();
app.AddEndpointsMusicas();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
