using ScreenSound.Shared.Models.Models;
using ScreenSoundAPI.Responses;

namespace ScreenSoundAPI.Converters
{
    public static class MusicasConverter
    {
        public static ICollection<MusicasResponse> ConvertMusicaListToMusicaResponseList(IEnumerable<Musica> musicas)
        {
            return musicas.Select(m => ConvertMusicaToMusicaResponse(m)).ToList();
        }

        public static MusicasResponse ConvertMusicaToMusicaResponse(Musica musica)
        {
            return new MusicasResponse(musica.Id,
                musica.Artista?.Id ?? 0,
                musica.Nome, 
                musica.Artista?.Nome ?? "Artista Desconhecido",
                musica.AnoLancamento, 
                GenerosConverter.ConvertGeneroListToGeneroResponseList(musica.Generos));
        }
    }
}
