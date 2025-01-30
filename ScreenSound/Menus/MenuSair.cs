using ScreenSound.Shared.Data.DataBase;
using ScreenSound.Shared.Models.Models;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(Repository<Artista> repository)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
