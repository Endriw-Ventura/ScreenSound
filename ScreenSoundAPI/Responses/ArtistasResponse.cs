namespace ScreenSoundAPI.Responses
{
    public record ArtistasResponse(int Id, string Nome, string Bio, string FotoPerfil, ICollection<MusicasResponse> Musicas);
}
