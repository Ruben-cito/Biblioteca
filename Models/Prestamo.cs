using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Prestamo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Fechaprestamo { get; set; }
        [Required]
        public DateTime Fechadevolucion { get; set; }
        [Required]
        public DateTime FechaConfirmacion { get; set; }
        public string? Estado { get; set; }

        public int? LibroId { get; set; }
        public virtual Libro? Libro { get; set; }
        public int? PersonaId { get; set; }
        public virtual Persona? Persona { get; set; }




    }
}
