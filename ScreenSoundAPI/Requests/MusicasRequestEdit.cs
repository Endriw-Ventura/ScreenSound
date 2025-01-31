using System.ComponentModel.DataAnnotations;

namespace ScreenSoundAPI.Requests
{
    public record MusicasRequestEdit([Required] int Id, [Required] string Nome, string AnoLancamento);
}
