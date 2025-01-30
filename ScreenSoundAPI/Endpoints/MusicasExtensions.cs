using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScreenSound.Shared.Data.DataBase;
using ScreenSound.Shared.Models.Models;
using ScreenSoundAPI.DTOs;
using ScreenSoundAPI.Requests;
using ScreenSoundAPI.Responses;

namespace ScreenSoundAPI.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndpointsMusicas(this WebApplication app)
        {

            app.MapGet("/Musicas", (Repository<Musica> db) =>
            {
                var musicas = db.GetAll();
                return Results.Ok(EntityListToReponseList(musicas));
            });

            app.MapGet("/Musicas/{nome}", (Repository<Musica> db, string nome) =>
            {
                var musica = db.Get(a => a.Nome.ToLower().Equals(nome.ToLower()));
                if (musica is null)
                    return Results.NotFound();

                return Results.Ok(EntityToResponse(musica));
            });

            app.MapPost("/Musicas", (Repository<Musica> db, [FromBody] MusicasRequest musica) =>
            {
                var musicaNova = new Musica(musica.Nome);
                db.Add(musicaNova);
                return Results.Ok(musicaNova.Id);
            });

            app.MapPut("/Musicas", (Repository<Musica> db, [FromBody] MusicasRequestEdit musica) =>
            {
                var musicaAntiga = db.Get(a => a.Id == musica.Id);
                if (musicaAntiga is null)
                    return Results.NotFound();

                if (!musica.Nome.IsNullOrEmpty())
                    musicaAntiga.Nome = musica.Nome;

                if (!musica.AnoLancamento.IsNullOrEmpty())
                    musicaAntiga.AnoLancamento = Convert.ToInt32(musica.AnoLancamento);

                db.Update(musicaAntiga);
                return Results.Ok(musicaAntiga);
            });

            app.MapDelete("/Musicas/{id}", (Repository<Musica> db, int id) =>
            {
                var musica = db.Get(a => a.Id == id);

                if (musica is null)
                    return Results.NotFound();

                db.Delete(musica);
                return Results.NoContent();
            });
        }
        private static ICollection<MusicasResponse> EntityListToReponseList(IEnumerable<Musica> artistas)
        {
            return artistas.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicasResponse EntityToResponse(Musica musica)
        {
            return new MusicasResponse(musica.Id,
                musica.Artista?.Id, 
                musica.Nome, 
                musica.Artista?.Nome ?? "Artista Desconhecido",
                musica.AnoLancamento);
        }
    }
}
