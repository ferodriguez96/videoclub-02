using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Models;

namespace VideoClub.DataBase
{
    public class VideoClubDbContext : DbContext
    {
        public VideoClubDbContext(DbContextOptions<VideoClubDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Alquiler> Alquileres { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
        public DbSet<PeliculaGenero> PeliculasGeneros { get; set; }
    }
}
