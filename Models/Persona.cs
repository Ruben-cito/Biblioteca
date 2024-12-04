using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Dto;

namespace Biblioteca.Models
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Paterno { get; set; }

        [Required]
        public string? Materno { get; set; }

        [Required, MinLength(3)]
        public string? Correo { get; set; }

        [Required, MinLength(3)]
        public string? Password { get; set; }


        public CargoEnum Cargo { get; set; }

        [Required]
        public string? Estado { get; set; }

        [Required]
        public string? Codigo { get; set; }

        [Required]
        public int Intentos { get; set; }

        [NotMapped]
        public string? NombreCompleto { get { return $"{Paterno} {Materno} {Nombre}"; } }
        
        [NotMapped]
        public string? CINombre { get { return $"{Codigo} - {Paterno} {Materno} {Nombre}"; } }

        public virtual List<Prestamo>? Prestamos { get; set; }
        public virtual List<Sancion>? Sanciones { get; set; }

    }
}
