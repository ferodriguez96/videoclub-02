using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoClub.Models
{
    public class Categoria
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Diseño")]
        public string Disenio { get; set; } //? supongo aca puedo clavarle el color de la cajita y que se refleje en el front-end

        [Required]
        [Display(Name = "Días de alquiler")]
        public int DiasDeAlquiler { get; set; }

        [Required]
        public float Precio { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
