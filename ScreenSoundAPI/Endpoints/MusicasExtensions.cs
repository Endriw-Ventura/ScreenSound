using Microsoft.AspNetCore.Mvc;
using ScreenSound.DataBase;
using ScreenSound.Modelos;

namespace ScreenSoundAPI.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndpointsMusicas(this WebApplication app)
        {

            app.MapGet("/Musicas", (Repository<Musica> db) =>
            {
                return Results.Ok(db.GetAll());
            });

            app.MapGet("/Musicas/{nome}", (Repository<Musica> db, string nome) => {
                var musica = db.Get(a => a.Nome.ToLower().Equals(nome.ToLower()));
                if (musica is null)
                    return Results.NotFound();

                return Results.Ok(musica);
            });

            app.MapPost("/Musicas", (Repository<Musica> db, [FromBody] Musica musica) =>
            {
                return Results.Ok(musica.Id);
            });

            app.MapPut("/Musicas", (Repository<Musica> db, [FromBody] Musica musica) => {
                var musicaAntiga = db.Get(a => a.Id == musica.Id);
                if (musicaAntiga is null)
                    return Results.NotFound();

                musicaAntiga.Nome = musica.Nome;
                musicaAntiga.AnoLancamento = musica.AnoLancamento;
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
    }
}
