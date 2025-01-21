using ScreenSound.DataBase;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(Repository<Artista> repository)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
