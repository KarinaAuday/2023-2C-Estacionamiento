using Microsoft.AspNetCore.Mvc;
using _2023_2C_F_Estacionamiento.Models;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    public class PersonasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index1()
        {
            return View();
        }

        public IActionResult  CrearPersona (String nombre , String apellido, int dni)
        {
            Persona persona = new Persona()

            {
                Apellido = apellido,
                Dni = dni,
                Nombre = nombre,
            };

            return View(persona);

        } 
    }


}
