using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Models;

namespace VideoClub.DataBase
{
    public static class DbInitializer
    {
        public static void Initialize(VideoClubDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Peliculas.Any())
            {
                return;   // DB has been seeded
            }
            Cliente cli = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nombre = "rodolfo",
                Apellido = "gomez",
                Dni ="test",
                Domicilio="calle falsa 123"
            };
            Categoria azul = new Categoria()
            {
                Id = Guid.NewGuid(),
                Disenio = "blue",
                DiasDeAlquiler = 7,
                Precio = 100,
                Descripcion = ""
            };
            Pelicula titanic = new Pelicula()
            {
                Id = Guid.NewGuid(),
                Titulo = "Titanic",
                AnioLanzamiento = 1994,
                Duracion = 190,
                Stock = 5,
                Categoria = azul
            };
            //context.Clientes.Add(cli);
            //context.Categorias.Add(azul);
            context.SaveChanges();
        }
    }
}
