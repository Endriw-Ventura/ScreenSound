using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScreenSound.Shared.Data.DataBase;
using ScreenSound.Shared.Models.Models;
using ScreenSoundAPI.Converters;
using ScreenSoundAPI.DTOs;
using ScreenSoundAPI.Requests;
using ScreenSoundAPI.Responses;

namespace ScreenSoundAPI.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndpointsMusicas(this WebApplication app)
        {

            var groupBuilder = app.MapGroup("musicas")
                .RequireAuthorization()
                .WithTags("Musicas");

            groupBuilder.MapGet("", (Repository<Musica> db) =>
            {
                var musicas = db.GetAll();
                return Results.Ok(MusicasConverter.ConvertMusicaListToMusicaResponseList(musicas));
            });

            groupBuilder.MapGet("{nome}", (Repository<Musica> db, string nome) =>
            {
                var musica = db.Get(a => a.Nome.ToLower().Equals(nome.ToLower()));
                if (musica is null)
                    return Results.NotFound();

                return Results.Ok(MusicasConverter.ConvertMusicaToMusicaResponse(musica));
            });

            groupBuilder.MapPost("", (Repository<Musica> db, [FromBody] MusicasRequest musica) =>
            {
                var musicaNova = new Musica(musica.Nome);
                db.Add(musicaNova);
                return Results.Ok(musicaNova.Id);
            });

            groupBuilder.MapPut("", (Repository<Musica> db, [FromBody] MusicasRequestEdit musica) =>
            {
                var musicaAntiga = db.Get(a => a.Id == musica.Id);
                if (musicaAntiga is null)
                    return Results.NotFound();

                musicaAntiga.Nome = musica.Nome;

                if (!musica.AnoLancamento.IsNullOrEmpty())
                    musicaAntiga.AnoLancamento = Convert.ToInt32(musica.AnoLancamento);

                db.Update(musicaAntiga);
                return Results.Ok(musicaAntiga);
            });

            groupBuilder.MapDelete("{id}", (Repository<Musica> db, int id) =>
            {
                var musica = db.Get(a => a.Id == id);

                if (musica is null)
                    return Results.NotFound();

                db.Delete(musica);
                return Results.NoContent();
            });

            groupBuilder.MapPatch("{nome}/Generos", (
                Repository<Musica> musicaDB,
                Repository<Genero> generoDB, 
                string nome, 
                [FromBody] List<int> generosIDs) => {

                    var musica = musicaDB.Get(m => m.Nome.ToLower().Equals(nome.ToLower()));
                    if (musica is null)
                        return Results.NotFound("Musica não encontrada");

                    var generos = new List<Genero>();

                    foreach (int id in generosIDs)
                    {
                        var genero = generoDB.Get(g => g.Id == id);
                        if (genero is null)
                            return Results.NotFound("Genero não encontrado");

                        generos.Add(genero);
                    }

                    musica.Generos = generos;
                    musicaDB.Update(musica);
                    return Results.Ok();
            });
        }
    }
}
