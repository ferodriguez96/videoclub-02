using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VideoClub.Models
{
    public class PeliculaGenero
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Pelicula")]
        public Guid PeliculaId { get; set; }
        public virtual Pelicula Pelicula { get; set; }

        [ForeignKey("Genero")]
        public Guid GeneroId { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
