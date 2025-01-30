using Microsoft.EntityFrameworkCore;
using ScreenSound.Shared.Models.Models;

namespace ScreenSound.Shared.Data.DataBase
{
    public class ScreenSoundContext : DbContext
    {
        private readonly string _connectionString = "Server=localhost;Database=ScreenSoundDB;User Id=admin;Password=123;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString).UseLazyLoadingProxies(); ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artista>().HasMany(a => a.Musicas).WithOne(m => m.Artista);
            modelBuilder.Entity<Musica>().HasMany(m => m.Generos).WithMany(g => g.Musicas);
        }
    }
}
