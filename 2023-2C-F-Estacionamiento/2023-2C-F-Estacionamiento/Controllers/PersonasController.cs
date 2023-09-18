using _2023_2C_F_Estacionamiento.Data;
using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    public class PersonasController : Controller
    {

        //Creo un DB context. Fuerzo a recibir un contexto de base de datos
        private readonly EstacionamientoContext _contexto;

        public PersonasController(EstacionamientoContext contexto)
        {
            this._contexto = contexto;
        }


        public IActionResult Index()
        {
            List<Persona> personasEnDb = _contexto.Personas.ToList();

            return View(personasEnDb);

            //Me manda a Crear 
            //return RedirectToAction("Crear");
        }

        public ActionResult Create()
        {
            

            return View();

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

           // _contexto.Add(persona);
            //_contexto.SaveChanges();

            //Responde un Texto plano
            // return Content($"OK se creo a la Persona {persona.NombreCompleto} y el Id es {persona.Id} ");

            //Le mando la Persona como modelo
            return View(persona);
        }


        //Creo una persona y la guardo en la Base de Datos  . Si no le madno mail me da error en al base de datos al guardar pues es un dato Required
        public ActionResult Crear2(string nombre="Luis Alberto", string apellido = "Spinetta" , string email = "Luis@a.com")
        {
            Persona persona = new Persona()
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email
                // Dni = dni.Value  // pongo .value cuando el valor permite null. En este caso acepto null , 
            };

            _contexto.Add(persona);
            _contexto.SaveChanges();
            

            //Responde un Texto plano
            // return Content($"OK se creo a la Persona {persona.NombreCompleto} y el Id es {persona.Id} ");

            //Le mando la Persona como modelo
            return RedirectToAction("Index");
        }

        public IActionResult Repository()
        {
            //  creo las Personas que traigo de la DB y la hago tolist

            //  List<Persona> Personas = _contexto.Personas.ToList();

            var personas = new PersonasRepository();
            //lista de personas

            //Lo guardo uno a uno en la base de datos
            foreach (Persona persona  in personas.Personas)
            {
                _contexto.Add(persona);
                _contexto.SaveChanges();
            }
           
           

            return View(personas.Personas);

           

            // return View(_contexto.Personas);
        }



    }
}
