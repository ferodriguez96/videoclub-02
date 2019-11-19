using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    public class Pelicula
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Título")]
        [Required]
        public string Titulo { get; set; }

        [Display(Name = "Duración")]
        [Required]
        public int Duracion { get; set; } //En minutos

        [Required]
        public int Stock { get; set; }

        [Display(Name = "Año de lanzamiento")]
        public int AnioLanzamiento { get; set; }


        [ForeignKey("Categoria")]
        [Display(Name = "Categoría ID")]
        [Required]
        public virtual Guid CategoriaId { get; set; } //En general, nada que ver con el genero de la pelicula

        public virtual Categoria Categoria { get; set; } //En general, nada que ver con el genero de la pelicula

        public virtual ICollection<Alquiler> Alquileres { get; set; }

        public virtual ICollection<PeliculaGenero> PeliculaGeneros { get; set; }
    }
}
