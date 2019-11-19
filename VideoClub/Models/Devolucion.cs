using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoClub.Models
{
    public class Devolucion
    {
        [Key]
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de devolución")]
        public DateTime FechaDevolucion { get; set; }

        [Display(Name = "Precio final")]
        public float PrecioFinal { get; set; }

        [ForeignKey("Alquiler")]
        [Display(Name = "Alquiler ID")]
        public Guid AlquilerId { get; set; }

        public virtual Alquiler Alquiler { get; set; }
    }
}
