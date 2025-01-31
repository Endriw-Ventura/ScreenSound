using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Shared.Models.Models
{
    public class Genero
    {
        public Genero() { }
        public Genero(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
    }
}
