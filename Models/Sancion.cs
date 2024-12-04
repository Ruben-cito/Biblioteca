using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Sancion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Montototal { get; set; }

        [Required]
        public string? Motivo { get; set; }

        public int PersonaId { get; set; }
        public virtual Persona? Persona { get; set; }
        
        public int PrestamoId { get; set; }

        public virtual Prestamo? Prestamo { get; set; }
    }
}
