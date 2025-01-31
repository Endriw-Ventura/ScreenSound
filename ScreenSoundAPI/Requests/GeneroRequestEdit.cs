using System.ComponentModel.DataAnnotations;

namespace ScreenSoundAPI.Requests
{
    public record GeneroRequestEdit([Required] int Id, [Required] string Nome, string Descricao);
}
