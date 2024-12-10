using Biblioteca.Models;

namespace Biblioteca.Dto
{

    public class LibroX {
        public string? Titulo { get; set; }
        public int Cantidad { get; set; }
    }
    public class ReporteLibros
    {
        public List<LibroX>? Libros { get; set; }
    }
}
