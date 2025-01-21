using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.DataBase
{
    internal class ScreenSoundContext: DbContext
    {
        private readonly string _connectionString = "Server=localhost;Database=ScreenSoundDB;User Id=admin;Password=123;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString).UseLazyLoadingProxies(); ;
        }
    }
}
