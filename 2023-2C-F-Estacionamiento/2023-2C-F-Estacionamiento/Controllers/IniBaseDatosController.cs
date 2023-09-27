using _2023_2C_F_Estacionamiento.Data;
using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace _2023_2C_F_Estacionamiento.Controllers
{
    public class IniBaseDatosController : Controller
    {

        private readonly EstacionamientoContext _context;
        public IniBaseDatosController (EstacionamientoContext contexto)
        {
            this._context = contexto;
        }
        public IActionResult Incializar()
        {
           // IncializarPersonas();
            IncializarClientes();
            //va al index del controlador Personas1
            return RedirectToAction("Index", "Personas");
        }

        private void IncializarClientes()
        {
            if (!_context.Personas.Any())
            {

                //voy a crear una persona a
                Cliente cliente = new Cliente()
                {
                    Nombre = "Charly",
                    Apellido = "Garcia",
                    Dni = 55667788,
                    Email = "charly@ort.edu.ar"

                };
                _context.Personas.Add(cliente);
                _context.SaveChanges();

                 Cliente cliente2 = new Cliente()
                {

                    Nombre = "Luis",
                    Apellido = "Alberto Spinetta",
                    Dni = 55228788,
                    Email = "LASy@ort.edu.ar"

                };
                _context.Personas.Add(cliente2);
                _context.SaveChanges();

                Cliente cliente3 = new Cliente()
                { Nombre = "Gustavo", Apellido = "Cerati", Dni = 66228788, Email = "Cerati@gmail.com" };
                _context.Personas.Add(cliente3);
                _context.SaveChanges();

                Cliente cliente4 = new Cliente() { Nombre = "Astor", Dni = 67728788, Apellido = "Piazolla", Email = "¨Piazolla@gmail.com" };
                _context.Personas.Add(cliente4);
                _context.SaveChanges();

                Cliente cliente5 = new Cliente() { Nombre = "Miguel", Apellido = "Mateos", Dni = 67728788, Email = "¨Mateos@gmail.com" };

                _context.Personas.Add(cliente5);
                _context.SaveChanges();


                Cliente cliente6 = new Cliente() { Nombre = "Juan Carlos", Apellido = "Baglietto", Dni = 67528788, Email = "¨Mateos@gmail.com" };

                _context.Personas.Add(cliente6);
                _context.SaveChanges();
            }
        }

        private void IncializarPersonas()
        {
            #region primer persona prueba
            //pregunto a ver si hay algo en el contexto. aca me crea una primera persona si no hay niguna
            if (!_context.Personas.Any())
            {

                //voy a crear una persona a
                Persona persona = new Persona()
                {
                    Nombre = "Charly",
                    Apellido = "Garcia",
                    Dni = 55667788,
                    Email = "charly@ort.edu.ar"

                };
                _context.Personas.Add(persona);
                _context.SaveChanges();

                Persona persona2 = new Persona()
                {

                    Nombre = "Luis",
                    Apellido = "Alberto Spinetta",
                    Dni = 55228788,
                    Email = "LASy@ort.edu.ar"

                };
                _context.Personas.Add(persona2);
                _context.SaveChanges();

                 Persona persona3 = new Persona() 
                 { Nombre = "Gustavo", Apellido = "Cerati", Dni = 66228788, Email = "Cerati@gmail.com" };
                _context.Personas.Add(persona3);
                _context.SaveChanges();

                Persona persona4 = new Persona() { Nombre = "Astor", Dni = 67728788, Apellido = "Piazolla", Email = "¨Piazolla@gmail.com" };
                _context.Personas.Add(persona4);
                _context.SaveChanges();

                Persona persona5 = new Persona() { Nombre = "Miguel", Apellido = "Mateos", Dni = 67728788, Email = "¨Mateos@gmail.com" };
                
                _context.Personas.Add(persona5);
                _context.SaveChanges();


                Persona persona6 = new Persona() { Nombre = "Juan Carlos", Apellido = "Baglietto", Dni = 67528788, Email = "¨Mateos@gmail.com" };

                _context.Personas.Add(persona6);
                _context.SaveChanges();
            }
        }
        #endregion

    }
}
