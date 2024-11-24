using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

namespace Biblioteca.Contexto
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Dato> Datos { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Sancion> Sanciones { get; set; }
    }
}
