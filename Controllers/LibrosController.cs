using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Contexto;
using Biblioteca.Models;
using Microsoft.VisualBasic;

namespace Biblioteca.Controllers
{
    public class LibrosController : Controller
    {
                
        private readonly MyContext _context;


        //codigo para foto 3
       
        private readonly IWebHostEnvironment _webHostEnvironment;



        // codigo para fotos 4                    ,IWebHostEnvironment webHostEnvironment

        public LibrosController(MyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;

        //codigo para foto 5 inicializar

            _webHostEnvironment = webHostEnvironment;
        }



        // GET: Libros
        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros.Include(p => p.Autor).Include(p => p.Categoria).Include(p => p.Editorial).ToListAsync();
           
            return View(libros);

        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            //filtros
            ViewData["Autores"] = new SelectList(_context.Datos.Where(D => D.Tipo == Dto.TipoDatoDto.Autor), "Id", "Nombre");
            ViewData["Categorias"] = new SelectList(_context.Datos.Where(D => D.Tipo == Dto.TipoDatoDto.Categoria), "Id", "Nombre");
            ViewData["Editoriales"] = new SelectList(_context.Datos.Where(D => D.Tipo == Dto.TipoDatoDto.Editorial), "Id", "Nombre");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        //public async Task<IActionResult> Create([Bind("Id,Titulo,ISBN,Imagenportada,Ubicacion,Ejemplares,Estado,Fechareg,AutorId,CategoriaId,EditorialId")] Libro libro)
        public async Task<IActionResult> Create([Bind("Titulo,ISBN,FotoFile,Ubicacion,Ejemplares,Estado,Fechareg,AutorId,CategoriaId,EditorialId")] Libro libro)
        {
            

            if (ModelState.IsValid)
            {
                //codigo para foto 1
                Console.WriteLine("subir archivo");

                if (libro.FotoFile!=null)
                {
                 
                    await subirArchivo(libro);
                }

                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        //codigo para foto 2 pero antes tenemos q crear el codigo 3

        private async Task subirArchivo(Libro libro)
        {
            string rootPath = _webHostEnvironment.WebRootPath;
            //para cualquier extension
            string extension = Path.GetExtension(libro.FotoFile!.FileName);
            //renombrar a un nombre aleatorio
            Guid randomGuid= Guid.NewGuid();
            //generar el nombre y poner la extension 
            string nombreArchivo=$"{randomGuid}{extension}";


// guardar el nombre del archivo sera en el campo imagenportada en la tabla osea el el models atributo imagenportada
        libro.Rutaportada=nombreArchivo;

            //guardar la foto en roo en la carpeta imgPortada mas el nombrearchivo

            string path = Path.Combine($"{rootPath}/imgPortada/", nombreArchivo);
            var fileStream = new FileStream(path, FileMode.Create);
            await libro.FotoFile.CopyToAsync(fileStream);
        }



        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,ISBN,Rutaportada,Imagenportada,Ubicacion,Ejemplares,Estado,Fechareg,AutorId,CategoriaId,EditorialId")] Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
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
            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}
