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
    public class ClienteVehiculosController : Controller
    {
        private readonly EstacionamientoContext _context;

        public ClienteVehiculosController(EstacionamientoContext context)
        {
            _context = context;
        }

        // GET: ClienteVehiculos
        public async Task<IActionResult> Index()
        {
            var estacionamientoContext = _context.ClientesVehiculo.Include(c => c.Cliente).Include(c => c.Vehiculo);
            return View(await estacionamientoContext.ToListAsync());
        }

        // GET: ClienteVehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientesVehiculo == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _context.ClientesVehiculo
                .Include(c => c.Cliente)
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }

            return View(clienteVehiculo);
        }

        // GET: ClienteVehiculos/Create
        public IActionResult Create(int ClienteId)
        {


            //ViewData["ClienteId"] = new SelectList(_context.Cliente.Where(c=>c.Id == ClienteId), "Id", "NombreCompleto");
            ViewData["ClienteId"] = ClienteId;
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculo, "Id", "Patente");
            ClienteVehiculo clienteVehiculo = new ClienteVehiculo();
            clienteVehiculo.ClienteId = ClienteId;



            return View(clienteVehiculo);
        }

        // POST: ClienteVehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ClienteId,VehiculoId,ResponsablePrincipal")] ClienteVehiculo clienteVehiculo)
        {
            clienteVehiculo.Cliente = _context.Cliente.Find(clienteVehiculo.ClienteId);
            clienteVehiculo.Vehiculo = _context.Vehiculo.Find(clienteVehiculo.VehiculoId);
            


            if (ModelState.IsValid)
            {
                _context.Add(clienteVehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else

                ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Discriminator", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculo, "Id", "Color", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // GET: ClienteVehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientesVehiculo == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _context.ClientesVehiculo.FindAsync(id);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Discriminator", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculo, "Id", "Color", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // POST: ClienteVehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,VehiculoId,ResponsablePrincipal")] ClienteVehiculo clienteVehiculo)
        {
            if (id != clienteVehiculo.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteVehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteVehiculoExists(clienteVehiculo.ClienteId))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Discriminator", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculo, "Id", "Color", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // GET: ClienteVehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientesVehiculo == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _context.ClientesVehiculo
                .Include(c => c.Cliente)
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }

            return View(clienteVehiculo);
        }

        // POST: ClienteVehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientesVehiculo == null)
            {
                return Problem("Entity set 'EstacionamientoContext.ClientesVehiculo'  is null.");
            }
            var clienteVehiculo = await _context.ClientesVehiculo.FindAsync(id);
            if (clienteVehiculo != null)
            {
                _context.ClientesVehiculo.Remove(clienteVehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteVehiculoExists(int id)
        {
            return (_context.ClientesVehiculo?.Any(e => e.ClienteId == id)).GetValueOrDefault();
        }



        public IActionResult AsignarVehiculo(int ClienteId)
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Discriminator");
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculo, "Id", "Color");
            return View();
        }
    }
}
