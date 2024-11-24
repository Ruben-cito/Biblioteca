using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Titulo { get; set; }

        public string? ISBN { get; set; }



        //[Required]
        public string? Rutaportada { get; set; }

      
        //almacenar foto

        //cargar foto
        [NotMapped]
        [Display(Name = "Cargar Foto")]
        public IFormFile? FotoFile { get; set; }



        [Required]
        public string? Ubicacion { get; set; }
        [Required]
        public int Ejemplares { get; set; }
        [Required]
        public string? Estado { get; set; }

        [Required]
        public DateTime Fechareg { get; set; }
        
        public int? AutorId { get; set; }
        public virtual Dato? Autor { get; set; }
        public int? CategoriaId { get; set; }
        public virtual Dato? Categoria { get; set; }
        public int? EditorialId { get; set; }
        public virtual Dato? Editorial { get; set; }



        public virtual List<Prestamo>? Prestamos { get; set; }

    }
}
