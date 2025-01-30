using ScreenSound.Shared.Data.DataBase;
using ScreenSound.Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus
{
    internal class MenuMostrarMusicasLancamento : Menu
    {
        public override void Executar(Repository<Artista> repository)
        {
            base.Executar(repository);
            ExibirTituloDaOpcao("Exibir detalhes do artista");
            Console.Write("Digite o ano de lançamento: ");
            string anoLancamento = Console.ReadLine()!;
            var musicaRepository = new Repository<Musica>(new ScreenSoundContext());
            var musicas = musicaRepository.GetAll(x => x.AnoLancamento == Convert.ToInt32(anoLancamento));
            if (musicas is not null && musicas.Any())
            {
                Console.WriteLine($"\n Musicas do ano {anoLancamento}:");

                foreach (var musica in musicas)
                {
                    Console.WriteLine($"Música: {musica.Nome}, Artista:{musica.Artista!.Nome}, Ano de lançamento: {musica.AnoLancamento}");
                }
                Console.WriteLine("\n Digite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"\n Não foram encontradas músicas neste ano de lançamento!");
                Console.WriteLine("Digite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
