using _2023_2C_F_Estacionamiento.Data;
using _2023_2C_F_Estacionamiento.Herlpers;
using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly List<string> roles = new List<string>() { Configs.EmpleadoRolName, Configs.ClienteRolName, Configs.AdminRolName };
        private List<Empleado> empleados = new List<Empleado>()
        {
            new Empleado("123" , "Kari" , "Auday",22334455 , "kauday@gmail.com"),
            new Empleado("124", "Juan", "Perez", 34998855, "fabala@gmail.com"),
        };
        private List<Vehiculo> vehiculos = new List<Vehiculo>()
        {
            new Vehiculo(2034444,"Ford taunus" , "Verde"),
            new Vehiculo(8484848, "Renault Clio" , "Azul") ,
            new Vehiculo(5647866, "Mercedes benz" , "amarillo"),
        };

        public IniBaseDatosController(UserManager<Persona> userManager, RoleManager<Rol> roleManager, EstacionamientoContext contexto)
        {
            //Agrego usario y roles
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = contexto;
        }
        public IActionResult Incializar()
        {
            CrearRoles().Wait();
            CrearEmpleados().Wait();
            IncializarClientes().Wait();
            CrearVehiculos();
            //va al index del Home, Y manda este mensaje por ViewBag
            return RedirectToAction("Index", "Home", new { mensaje = "PreCarga de Base de Datos finalizada" });
        }

        private  void CrearVehiculos()
        {
            foreach (var vehiculo in vehiculos)
            {
                if (!_context.Vehiculo.Any(e => e.Patente == vehiculo.Patente))
                {
                    _context.Vehiculo.Add(vehiculo);
                    _context.SaveChanges();
                }

            }
        }

        private async Task CrearEmpleados()
        {
            foreach (var empleado in empleados)
            {
                if (!_context.Empleado.Any(e => e.Email == empleado.Email))
                {
                    empleado.UserName = empleado.Email;
                    if (empleado.Apellido.Equals("Auday"))
                    {

                        await _userManager.CreateAsync(empleado, Configs.PasswordGenerica);
                        await _userManager.AddToRoleAsync(empleado, "Admin");
                    }
                    else
                    {
                        await _userManager.CreateAsync(empleado, Configs.PasswordGenerica);
                        await _userManager.AddToRoleAsync(empleado, "Empleado");
                    }
                }

            }

        }

        private async Task CrearRoles()
        {
            foreach (var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                    await _roleManager.CreateAsync(new Rol(rolName));
                }
            }
        }

        private async Task IncializarClientes()
        {
            if (!_context.Cliente.Any())
            {

                Cliente cliente = new Cliente()
                {
                    Nombre = "Charly",
                    Apellido = "Garcia",
                    Dni = 55997788,
                    Email = "charly@ort.edu.ar",
                    Cuil = 55997788

                };
                cliente.UserName = cliente.Email;
                await _userManager.CreateAsync(cliente, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(cliente, Configs.ClienteRolName);
                //_context.Cliente.Add(cliente);
                //_context.SaveChanges();

                Direccion direccion = new Direccion()

                {
                    Calle = "Mansilla",
                    Numero = 232,
                    CodPostal = 1425,
                    Id = cliente.Id
                };

                _context.Direcciones.Add(direccion);
                _context.SaveChanges();

                Cliente cliente2 = new Cliente()
                {

                    Nombre = "Luis",
                    Apellido = "Alberto Spinetta",
                    Dni = 55228788,
                    Email = "LASy@ort.edu.ar",
                    Cuil = 55667788
                };
                cliente2.UserName = cliente2.Email;
                await _userManager.CreateAsync(cliente2, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(cliente2, "Cliente");

                Direccion direccion2 = new Direccion()

                {
                    Calle = "Charcas",
                    Numero = 232,
                    CodPostal = 1425,
                    Id = cliente2.Id
                };

                _context.Direcciones.Add(direccion2);
                _context.SaveChanges();

                Cliente cliente3 = new Cliente()
                { Nombre = "Gustavo", Apellido = "Cerati", Dni = 66228788, Email = "Cerati@gmail.com", Cuil = 55667777 };
                cliente3.UserName = cliente3.Email;
                await _userManager.CreateAsync(cliente3, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(cliente3, "Cliente");

                Direccion direccion3 = new Direccion()

                {
                    Calle = "Callao",
                    Numero = 111,
                    CodPostal = 1425,
                    Id = cliente3.Id
                };

                _context.Direcciones.Add(direccion3);
                _context.SaveChanges();




                Cliente cliente4 = new Cliente() { Nombre = "Astor",  Apellido = "Piazolla", Dni = 57728788, Email = "Piazolla@gmail.com", Cuil = 55007777 };
                cliente4.UserName = cliente4.Email;
                await _userManager.CreateAsync(cliente4, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(cliente4, "Cliente");

                Direccion direccion4 = new Direccion()

                {
                    Calle = "Corrientes",
                    Numero = 444,
                    CodPostal = 1425,
                    Id = cliente4.Id
                };

                _context.Direcciones.Add(direccion4);
                _context.SaveChanges();

                Cliente cliente5 = new Cliente() { Nombre = "Miguel", Apellido = "Mateos", Dni = 87728788, Email = "Mateos@gmail.com", Cuil = 66007777 };
                cliente5.UserName = cliente5.Email;
                await _userManager.CreateAsync(cliente5, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(cliente5, "Cliente");
                Direccion direccion5 = new Direccion()

                {
                    Calle = "San Juan",
                    Numero = 4545,
                    CodPostal = 1425,
                    Id = cliente5.Id
                };

                _context.Direcciones.Add(direccion5);
                _context.SaveChanges();

                Cliente cliente6 = new Cliente() { Nombre = "Juan Carlos", Apellido = "Baglietto", Dni = 97528788, Email = "Baglieto@gmail.com", Cuil = 55009999 };
                cliente6.UserName = cliente6.Email;
                await _userManager.CreateAsync(cliente6, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(cliente6, "Cliente");

                Direccion direccion6 = new Direccion()

                {
                    Calle = "San Juan",
                    Numero = 4545,
                    CodPostal = 1425,
                    Id = cliente6.Id
                };

                _context.Direcciones.Add(direccion6);
                _context.SaveChanges();
                //_context.Cliente.Add(cliente6);
                //_context.SaveChanges();
            }
        }

        private void IncializarPersonas()
        {

            //pregunto a ver si hay algo en el contexto. aca me crea una primera persona si no hay niguna
            if (!_context.Personas.Any())
            {
                #region Creacion Clientes
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
                #endregion 
            }
        }




    }
}
