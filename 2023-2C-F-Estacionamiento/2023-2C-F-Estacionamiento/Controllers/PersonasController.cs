using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    public class PersonasController : Controller
    {
        public IActionResult Index()
        {

            return View();

            //Me manda a Crear 
            //return RedirectToAction("Crear");
        }

        //Si no me pasan el dato que tiene el signo de parentesis me llega null , lo hago para que me envie cero si es un dato valido ya que el int lo inciializa en 0
        public ActionResult Crear(string nombre , string apellido,int id , int ?dni )
        {
            Persona persona = new Persona()
            { Nombre = nombre,
                Apellido = apellido,
                Id = id,
             // Dni = dni.Value  // pongo .value cuando el valor permite null. En este caso acepto null , 
            };

            //Responde un Texto plano
            // return Content($"OK se creo a la Persona {persona.NombreCompleto} y el Id es {persona.Id} ");

            //Le mando la Persona como modelo
            return View(persona);
        } 
        
        
        
        
    }
}
