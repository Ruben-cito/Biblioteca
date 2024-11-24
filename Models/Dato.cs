using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Biblioteca.Dto;

namespace Biblioteca.Models
{
    public class Dato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Estado { get; set; }
        [Required]
         public TipoDatoDto Tipo { get; set; }
    }
}
