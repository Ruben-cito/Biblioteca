using Biblioteca.Models;

namespace Biblioteca.Dto
{
    public class HistorialLector
    {
        public Persona? Persona { get; set; }
        public List<Prestamo>? Prestamos { get; set; }
        public List<Sancion>? Sanciones { get; set; }
    }
}
