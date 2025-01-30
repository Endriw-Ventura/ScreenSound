using ScreenSound.Shared.Data.DataBase;
using ScreenSound.Shared.Models.Models;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicas : Menu
{
    public override void Executar(Repository<Artista> repository)
    {
        base.Executar(repository);
        ExibirTituloDaOpcao("Exibir detalhes do artista");
        Console.Write("Digite o nome do artista que deseja conhecer melhor: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artista = repository.Get((x) => x.Nome.Equals(nomeDoArtista));
        if (artista is not null)
        {
            Console.WriteLine("\nDiscografia:");
            artista.ExibirDiscografia();
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
