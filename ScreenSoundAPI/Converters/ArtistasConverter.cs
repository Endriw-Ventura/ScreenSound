using ScreenSound.Shared.Models.Models;
using ScreenSoundAPI.Responses;

namespace ScreenSoundAPI.Converters
{
    public class ArtistasConverter
    {
        public static ICollection<ArtistasResponse> ConvertArtistaListToArtistaResponseList(IEnumerable<Artista> generos)
        {
            return generos.Select(a => ConvertArtistaToArtistaResponse(a)).ToList();
        }

        public static ArtistasResponse ConvertArtistaToArtistaResponse(Artista artista)
        {
            return new ArtistasResponse(
                artista.Id,
                artista.Nome,
                artista.FotoPerfil,
                artista.Bio,
                MusicasConverter.ConvertMusicaListToMusicaResponseList(artista.Musicas));
        }
    }
}
