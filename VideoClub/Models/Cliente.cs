using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoClub.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "maxlength exceded")]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "maxlength exceded")]
        public string Apellido { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "maxlength exceded")]
        public string Dni { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "maxlength exceded")]
        public string Domicilio { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "maxlength exceded")]
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Alquiler> Alquileres { get; set; }
    }
}
