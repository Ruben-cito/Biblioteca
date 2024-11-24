using Biblioteca.Contexto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NuGet.Protocol.Plugins;

namespace Biblioteca.Controllers
{
    public class LoginController : Controller
    {
        // interartuar con la base de datos p1
        private readonly MyContext _context;

        // interartuar con la base de datos p2
        public LoginController(MyContext context)
        {
            _context = context;
        }

        // interartuar con la base de datos p1
        public IActionResult Index()
        {

            return View();

        }

        //enviar datos del form login a la base de datos con el metodo [HttppPost] 

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string password)
        {
            //consultas landa con variable Persona para verificar los datos de correo y password que esta recibiendo
            var Persona = await _context.Personas
                .Where(x => x.Correo == correo && x.Password == password)
                .FirstOrDefaultAsync();
            //para verificar si existen los datos en la base de datos con la variable Persona
            if (Persona == null)
            {
                //Message("no existe datos");
                return RedirectToAction("Index");
            }
            else
            {
                if (Persona.Cargo == Dto.CargoEnum.Administrador)
                { 
                    return RedirectToAction("Index", "Libros");
                }
                else 
                {
                    if (Persona.Cargo == Dto.CargoEnum.Usuario)
                    { 
                        return RedirectToAction("index", "Datos");
                    }
                    else
                    {
                        return RedirectToAction("index", "Libros");
                    }
                }
            
            }
        }   
     
    }
}
