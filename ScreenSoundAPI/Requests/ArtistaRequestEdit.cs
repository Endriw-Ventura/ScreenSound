using System.ComponentModel.DataAnnotations;

namespace ScreenSoundAPI.Requests
{
    public record ArtistaRequestEdit([Required] int Id, [Required] string Nome, string Bio, string FotoPerfil);
}
