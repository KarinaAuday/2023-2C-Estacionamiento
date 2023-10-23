using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2023_2C_F_Estacionamiento.Data;
using _2023_2C_F_Estacionamiento.Models;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    public class ClientesController : Controller
    {
        private readonly EstacionamientoContext _context;

        public ClientesController(EstacionamientoContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return _context.Cliente != null ? View(await _context.Cliente.ToListAsync()) : Problem("Entity set 'EstacionamientoContext.Cliente'  is null.");
           
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            //Incluyo los objetos del contexto

            var cliente = await _context.Cliente
                                            .Include(clt => clt.Telefonos)
                                            .Include(clt => clt.Direccion)
                                            .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            var direccion = await _context.Direcciones
                .FirstOrDefaultAsync(m => m.Id == id);

            if (direccion == null)
            {
                return NotFound();
            }

            cliente.Direccion = direccion;
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cuil,Dni,Apellido,Nombre,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Direcciones", new { id = cliente.Id }); 
               // return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cuil,Id,Nombre,Apellido,Dni,Email")] Cliente clienteDelFormulario)
        {
            //Con ClienteFomulario Hacemos un mapeo Actualizando los campos que yo quiera
            if (id != clienteDelFormulario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var clienteEnDb = _context.Cliente.Find(clienteDelFormulario.Id);
                    if (clienteEnDb == null)
                    {
                        return NotFound();
                    }

                    clienteEnDb.Cuil = clienteDelFormulario.Cuil;
                    clienteEnDb.Dni = clienteDelFormulario.Dni;
                    clienteEnDb.Nombre = clienteDelFormulario.Nombre;
                    clienteEnDb.Apellido = clienteDelFormulario.Apellido;

                    if (!ActualizarEmail(clienteDelFormulario, clienteEnDb))
                    {
                        ModelState.AddModelError("Email", "El email ya está en uso");
                        return View(clienteDelFormulario);
                    }

                    _context.Update(clienteEnDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(clienteDelFormulario.Id))
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
            return View(clienteDelFormulario);
        }

        private bool ActualizarEmail(Cliente perForm, Cliente perDb)
        {
            bool resultado = true;

            try
            {
                if (!perDb.NormalizedEmail.Equals(perForm.Email.ToUpper()))
                {
                    //Si no son iguales proceso. verifico si ya existe el mail
                    if (_context.Personas.Any(p => p.NormalizedEmail == perForm.Email.ToUpper()))
                    {
                        resultado = false;
                    }
                    else
                    {
                        //como no existe actualizo
                        perDb.Email = perForm.Email;
                        perDb.NormalizedEmail = perForm.Email.ToUpper();
                        perDb.UserName = perForm.Email;
                        perDb.NormalizedUserName = perForm.NormalizedEmail;


                    }
                }


            }
            catch
            {
                resultado = false;
            }
            return resultado;

        }
    

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'EstacionamientoContext.Cliente'  is null.");
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return (_context.Cliente?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public ActionResult Buscar(string cli)
        {
           
            // Realiza la lógica de búsqueda utilizando el término "q".
            var resultados = _context.Cliente.Where(c => c.Apellido.Contains(cli)).ToList();

            // Devuelve la vista de resultados con la lista de resultados.
            return View("Buscador",resultados);
        }
    }
}
