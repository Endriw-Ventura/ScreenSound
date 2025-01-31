using System.ComponentModel.DataAnnotations;

namespace ScreenSoundAPI.Requests
{
    public record GeneroRequest([Required] string Nome, string Descricao);
}
