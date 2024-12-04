using Biblioteca.Models;

namespace Biblioteca.Dto
{
    public class CrearSancion
    {
        public Prestamo? Prestamo { get; set; }
        public Sancion? Sancion { get; set; }
    }
}
