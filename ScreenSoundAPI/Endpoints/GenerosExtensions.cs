using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScreenSound.Shared.Data.DataBase;
using ScreenSound.Shared.Models.Models;
using ScreenSoundAPI.Converters;
using ScreenSoundAPI.Requests;
using ScreenSoundAPI.Responses;

namespace ScreenSoundAPI.Endpoints
{
    public static class GenerosExtensions
    {
        public static void AddEndpointsGeneros(this WebApplication app)
        {

            var groupBuilder = app.MapGroup("generos")
                .RequireAuthorization()
                .WithTags("Generos");

            groupBuilder.MapGet("", (Repository<Genero> db) =>
            {
                var generos = db.GetAll();
                return Results.Ok(GenerosConverter.ConvertGeneroListToGeneroResponseList(generos));
            });

            groupBuilder.MapGet("{nome}", (Repository<Genero> db, string nome) =>
            {
                var genero = db.Get(g => g.Nome.ToLower().Equals(nome.ToLower()));
                if (genero is null)
                    return Results.NotFound();

                return Results.Ok(GenerosConverter.ConvertGeneroToGeneroResponse(genero));
            });

            groupBuilder.MapPost("", (Repository<Genero> db, [FromBody] GeneroRequest genero) => {
                var generoNovo = new Genero(genero.Nome, genero.Descricao);
                db.Add(generoNovo);
                return Results.Ok(generoNovo.Id);
            });

            groupBuilder.MapPut("", (Repository<Genero> db, [FromBody] GeneroRequestEdit genero) => {
                var generoAntigo = db.Get(g => g.Id == genero.Id);
                if (generoAntigo is null)
                    return Results.NotFound();

                generoAntigo.Nome = genero.Nome;

                if (!genero.Descricao.IsNullOrEmpty())
                    generoAntigo.Descricao = genero.Descricao;

                return Results.Ok();
            });

            groupBuilder.MapDelete("{id}", (Repository<Genero> db, int id) =>
            {
                var genero = db.Get(g => g.Id == id);
                if (genero is null)
                    return Results.NotFound();

                db.Delete(genero);
                return Results.NoContent();
            });
        }
    }
}
