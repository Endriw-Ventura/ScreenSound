﻿using System.ComponentModel.DataAnnotations;

namespace ScreenSoundAPI.Requests
{
    public record ArtistasRequest([Required] string Nome, string Bio);
}
