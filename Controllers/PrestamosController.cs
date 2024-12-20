﻿using System;
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
    public class PrestamosController : Controller
    {
        private readonly MyContext _context;

        public PrestamosController(MyContext context)
        {
            _context = context;
        }

        // [HttpGet()]
        public async Task<IActionResult> ReservacionesPdf()
        {
            var Prestamos = await _context.Prestamos
                .Include(x => x.Libro)
                .Include(x => x.Persona)
                .Where(x => x.Estado == "RESERVADO")
                .Select(p => new Prestamo {
                    Id = p.Id,
                    Libro = p.Libro,
                    Persona = p.Persona,
                    FechaConfirmacion = p.FechaConfirmacion,
                    Fechadevolucion = p.Fechadevolucion,
                    Fechaprestamo = p.Fechaprestamo,
                })
                .ToListAsync();
            return View(Prestamos);
        }

        public async Task<IActionResult> PrestamosRealizadosPdf()
        {
            var Prestamos = await _context.Prestamos
                .Include(x => x.Libro)
                .Include(x => x.Persona)
                .Where(x => x.Estado == "PRESTADO")
                .Select(p => new Prestamo {
                    Id = p.Id,
                    Libro = p.Libro,
                    Persona = p.Persona,
                    FechaConfirmacion = p.FechaConfirmacion,
                    Fechadevolucion = p.Fechadevolucion,
                    Fechaprestamo = p.Fechaprestamo,
                })
                .ToListAsync();
            return View(Prestamos);
        }

        public async Task<IActionResult> LibrosDevueltosPdf()
        {
            var Prestamos = await _context.Prestamos
                .Include(x => x.Libro)
                .Include(x => x.Persona)
                .Where(x => x.Estado == "DEVUELTO")
                .Select(p => new Prestamo {
                    Id = p.Id,
                    Libro = p.Libro,
                    Persona = p.Persona,
                    FechaConfirmacion = p.FechaConfirmacion,
                    Fechadevolucion = p.Fechadevolucion,
                    Fechaprestamo = p.Fechaprestamo,
                })
                .ToListAsync();
            return View(Prestamos);
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
           //var libros = await _context.Libros.Include(p => p.Id).ToListAsync();
           // Console.WriteLine(libros);
           // var personas = await _context.Personas.Include(p => p.Nombre).Include(p => p.Paterno).Include(p => p.Materno).ToListAsync();
            if(User.IsInRole("Lector")) {
                var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var prestamosLector = await _context.Prestamos
                    .Include(p => p.Libro)
                    .Include(p => p.Persona)
                    .Where(p => p.PersonaId == id)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();
                return View(prestamosLector);
            }
            else

            return View(await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Persona)
                .ToListAsync()
            );
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        [HttpGet("ObtenerLector")]
        public JsonResult ObtenerLector(int id)
        {
            Console.WriteLine($"ID recibido: {id}"); // Agrega un log para verificar

            var lector = _context.Personas
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Nombre,
                    p.Codigo,
                    p.Correo,
                })
                .FirstOrDefault();

            if (lector == null)
            {
                return Json(new { success = false, message = "Lector no encontrado" });
            }

            return Json(new { success = true, data = lector });
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewData["Libros"] = new SelectList(_context.Libros.Where(p => p.Ejemplares > 0), "Id", "Titulo");
       
            ViewData["Personas"] = new SelectList(_context.Personas.OrderBy(p => p.Codigo).Where(D => D.Cargo == Dto.CargoEnum.Lector), "Id", "CINombre");
            return View();
        }

        // POST: Prestamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fechaprestamo,Fechadevolucion,FechaConfirmacion,Estado,LibroId,PersonaId")] Prestamo prestamo)
        {
            prestamo.Fechaprestamo = DateTime.Now;
            prestamo.FechaConfirmacion = DateTime.Now;
            prestamo.Fechadevolucion = DateTime.Now;
            prestamo.Estado = "PRESTADO";
            if (ModelState.IsValid)
            {
                var libro = await _context.Libros.FindAsync(prestamo.LibroId);
                libro!.Ejemplares = libro.Ejemplares - 1;
                _context.Update(libro);

                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prestamo);
        }

        [HttpPost("Prestamos/Reservar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservar(int LibroId, int PersonaId)
        {
            var prestamo = new Prestamo();
            prestamo.LibroId = LibroId;
            prestamo.PersonaId = PersonaId;
            prestamo.Estado = "RESERVADO";
            prestamo.Fechaprestamo = DateTime.Now;
            prestamo.Fechadevolucion = DateTime.Now;
            prestamo.FechaConfirmacion = DateTime.Now;
            _context.Add(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Libros");
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> CrearReserva(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(x => x.Autor)
                .Include(x => x.Editorial)
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            var model = new CrearReserva() {
                Libro = libro,
                Prestamo = new Prestamo()
            };
            return View(model);
        }

        // Confirmar devolucion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarDevolucion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            prestamo.Fechadevolucion = DateTime.Now;
            prestamo.Estado = "DEVUELTO";
            var libro = await _context.Libros.FindAsync(prestamo.LibroId);
            libro!.Ejemplares = libro.Ejemplares + 1;
            _context.Update(libro);
            _context.Update(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Prestamos");
        }

        // Confirmar prestamo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarPrestamo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            prestamo.FechaConfirmacion = DateTime.Now;
            prestamo.Estado = "PRESTADO";
            var libro = await _context.Libros.FindAsync(prestamo.LibroId);
            libro!.Ejemplares = libro.Ejemplares - 1;
            _context.Update(libro);
            _context.Update(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Prestamos");
        }

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fechaprestamo,Fechadevolucion,FechaConfirmacion,Estado,LibroId,PersonaId")] Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
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
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
