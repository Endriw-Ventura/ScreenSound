using Microsoft.AspNetCore.Mvc;
using ScreenSound.DataBase;
using ScreenSound.Modelos;
using System.Runtime.CompilerServices;

namespace ScreenSoundAPI.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndpointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artistas", (Repository<Artista> db) =>
            {
                return Results.Ok(db.GetAll());
            });

            app.MapGet("/Artistas/{nome}", (Repository<Artista> db, string nome) => {
                var artista = db.Get(a => a.Nome.ToLower().Equals(nome.ToLower()));
                if (artista is null)
                    return Results.NotFound();

                return Results.Ok(artista);
            });

            app.MapPost("/Artistas", (Repository<Artista> db, [FromBody] Artista artista) =>
            {
                return Results.Ok(artista.Id);
            });

            app.MapPut("/Artistas", (Repository<Artista> db, [FromBody] Artista artista) => {
                var artistaAntigo = db.Get(a => a.Id == artista.Id);
                if (artistaAntigo is null)
                    return Results.NotFound();

                artistaAntigo.Nome = artista.Nome;
                artistaAntigo.FotoPerfil = artista.FotoPerfil;
                artistaAntigo.Bio = artista.Bio;
                db.Update(artistaAntigo);
                return Results.Ok(artistaAntigo);
            });

            app.MapDelete("/Artistas/{id}", (Repository<Artista> db, int id) =>
            {
                var artista = db.Get(a => a.Id == id);
                if (artista is null)
                    return Results.NotFound();

                db.Delete(artista);
                return Results.NoContent();
            });

        }
    }
}
