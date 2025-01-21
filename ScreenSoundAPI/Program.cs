using ScreenSound.DataBase;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/", () =>
{
    var db = new Repository<Artista>(new ScreenSoundContext());
    return db.GetAll();
});

app.Run();
