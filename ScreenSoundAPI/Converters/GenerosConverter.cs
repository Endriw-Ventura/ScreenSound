using ScreenSound.Shared.Models.Models;
using ScreenSoundAPI.Responses;

namespace ScreenSoundAPI.Converters
{
    public static class GenerosConverter
    {
        public static ICollection<GenerosResponse> ConvertGeneroListToGeneroResponseList(IEnumerable<Genero> generos)
        {
            return generos.Select(g => ConvertGeneroToGeneroResponse(g)).ToList();
        }

        public static GenerosResponse ConvertGeneroToGeneroResponse(Genero genero)
        {
            return new GenerosResponse(genero.Nome);
        }
    }
}
