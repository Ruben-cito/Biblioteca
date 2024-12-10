using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Contexto;
using Biblioteca.Models;
using Biblioteca.Dto;
using System.Security.Claims;

namespace Biblioteca.Controllers
{
    public class SancionesController : Controller
    {
        private readonly MyContext _context;

        public SancionesController(MyContext context)
        {
            _context = context;
        }

        // GET: Sanciones
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("Lector")) {
                var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var sancionesLector = await _context.Sanciones
                .Include(x => x.Persona)
                .Include(x => x.Prestamo)
                .Include(x => x.Prestamo!.Libro)
                .Where(x => x.PersonaId == id)
                .ToListAsync();
                return View(sancionesLector);
            }
            else
            return View(
                await _context.Sanciones
                .Include(x => x.Persona)
                .Include(x => x.Prestamo)
                .Include(x => x.Prestamo!.Libro)
                .ToListAsync()
            );
        }

        // GET: Sanciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sancion = await _context.Sanciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sancion == null)
            {
                return NotFound();
            }

            return View(sancion);
        }

        public async Task<IActionResult> CrearSancion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(x => x.Libro)
                .Include(x => x.Libro!.Autor)
                .Include(x => x.Persona)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }
            var model = new CrearSancion() {
                Prestamo = prestamo,
                Sancion = new Sancion()
            };
            return View(model);
        }

        [HttpPost("Sanciones/Crear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(int PrestamoId, int PersonaId, string motivo, int monto)
        {
            var sancion = new Sancion() {
                Montototal = monto,
                Motivo = motivo,
                PrestamoId = PrestamoId,
                PersonaId = PersonaId
            };
            var prestamo = await _context.Prestamos.FindAsync(PrestamoId);
            prestamo!.Estado = "DEVUELTO";
            var libro = await _context.Libros.FindAsync(prestamo.LibroId);
            libro!.Ejemplares = libro.Ejemplares + 1;
            _context.Update(prestamo);
            _context.Update(libro);
            _context.Add(sancion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Prestamos");
        }

        // GET: Sanciones/Create
        public IActionResult Create()
            // ViewData["Prestamos"] = new SelectList(_context.Datos.Where(D => D.Tipo == Dto.TipoDatoDto.), "Id", "Nombre");
            //ViewData["Personas"] = new SelectList(_context.Datos.Where(D => D.Tipo == Dto.TipoDatoDto.Categoria), "Id", "Nombre");
        {
            return View();
        }

        // POST: Sanciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Montototal,Motivo,PersonaId,Idprestamo")] Sancion sancion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sancion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sancion);
        }

        // GET: Sanciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sancion = await _context.Sanciones.FindAsync(id);
            if (sancion == null)
            {
                return NotFound();
            }
            return View(sancion);
        }

        // POST: Sanciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Montototal,Motivo,PersonaId,Idprestamo")] Sancion sancion)
        {
            if (id != sancion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sancion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SancionExists(sancion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sancion);
        }

        // GET: Sanciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sancion = await _context.Sanciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sancion == null)
            {
                return NotFound();
            }

            return View(sancion);
        }

        // POST: Sanciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sancion = await _context.Sanciones.FindAsync(id);
            if (sancion != null)
            {
                _context.Sanciones.Remove(sancion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SancionExists(int id)
        {
            return _context.Sanciones.Any(e => e.Id == id);
        }
    }
}
