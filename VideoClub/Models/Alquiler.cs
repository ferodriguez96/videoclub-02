using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    public class Alquiler
    {
        public Guid Id { get; set; }

        [Display(Name = "Pelicula")]
        public Guid PeliculaId { get; set; 
        }
        public virtual Pelicula Pelicula { get; set; }

        [ForeignKey("Cliente")]
        [Display(Name = "Cliente")]
        public Guid ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de alta")]
        public DateTime FechaAlta { get; set; }//fecha de alquiler

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de vencimiento")]
        public DateTime FechaVencimiento { get; set; }//Cuando deberia devolverlo

        [Display(Name = "Precio original")]
        public float PrecioOriginal { get; set; }

        [ForeignKey("Devolucion")]
        [Display(Name = "Devolución ID")]
        public Guid? DevolucionId { get; set; }

        public virtual Devolucion Devolucion { get; set;  }
    }
}
