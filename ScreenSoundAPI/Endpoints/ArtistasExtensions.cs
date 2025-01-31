using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScreenSound.Shared.Data.DataBase;
using ScreenSound.Shared.Models.Models;
using ScreenSoundAPI.Converters;
using ScreenSoundAPI.Requests;
using ScreenSoundAPI.Responses;

namespace ScreenSoundAPI.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndpointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artistas", (Repository<Artista> db) =>
            {
                var artistas = db.GetAll();
                return Results.Ok(ArtistasConverter.ConvertArtistaListToArtistaResponseList(artistas));
            });

            app.MapGet("/Artistas/{nome}", (Repository<Artista> db, string nome) => {
                var artista = db.Get(a => a.Nome.ToLower().Equals(nome.ToLower()));
                if (artista is null)
                    return Results.NotFound();

                return Results.Ok(ArtistasConverter.ConvertArtistaToArtistaResponse(artista));
            });

            app.MapPost("/Artistas", (Repository<Artista> db, [FromBody] ArtistasRequest artistaRequest) =>
            {
                var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);
                db.Add(artista);
                return Results.Ok(artista.Id);
            });

            app.MapPut("/Artistas", (Repository<Artista> db, [FromBody] ArtistaRequestEdit artista) => {
                var artistaAntigo = db.Get(a => a.Id == artista.Id);
                if (artistaAntigo is null)
                    return Results.NotFound();

                artistaAntigo.Nome = artista.Nome;

                if(!artista.FotoPerfil.IsNullOrEmpty())
                    artistaAntigo.FotoPerfil = artista.FotoPerfil;

                if(!artista.Bio.IsNullOrEmpty())
                    artistaAntigo.Bio = artista.Bio;

                db.Update(artistaAntigo);
                return Results.Ok();
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
