using Biblioteca.Models;

namespace Biblioteca.Dto
{
    public class CrearReserva
    {
        public Libro? Libro { get; set; }
        public Prestamo? Prestamo { get; set; }
    }
}
