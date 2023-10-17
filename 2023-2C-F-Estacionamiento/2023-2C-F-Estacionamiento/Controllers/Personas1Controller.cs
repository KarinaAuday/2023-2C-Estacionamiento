using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2023_2C_F_Estacionamiento.Data;
using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Identity;
using _2023_2C_F_Estacionamiento.Herlpers;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    public class Personas1Controller : Controller
    {
        private readonly EstacionamientoContext _context;
        private readonly UserManager<Persona> _userManager;

        //Adecuo para que le llegue el user a Persona
        public Personas1Controller(EstacionamientoContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Personas1
        public async Task<IActionResult> Index()
        {
              return _context.Personas != null ? 
                          View(await _context.Personas.ToListAsync()) :
                          Problem("Entity set 'EstacionamientoContext.Personas'  is null.");
        }

        // GET: Personas1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas1/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( bool EsAdmin ,[Bind("Id,Nombre,Apellido,Dni,Email")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                //Asigno el username con el email
                persona.UserName = persona.Email;
               var resultadopersona = await _userManager.CreateAsync(persona, Configs.PasswordGenerica);
               
                if (resultadopersona.Succeeded)
                {
                    IdentityResult resultadoAddRole;
                    string rolDefinido;
                    //le agrego el rol correspondiente
                    if (EsAdmin)
                    {
                        //Agrego rol Admin
                        rolDefinido = Configs.AdminRolName;
 
            
                    } 
                    else
                    {
                        //Agrego rol usuario
                        rolDefinido = Configs.ClienteRolName;

                    }
                    resultadoAddRole = await _userManager.AddToRoleAsync(persona, rolDefinido);
                    if (resultadoAddRole.Succeeded)
                    {
                        return RedirectToAction("index", "Personas1");
                    }
                    else
                    {
                        return Content($"No se ha podido agregar el rol{rolDefinido}");
                    }

                }

                //_context.Add(persona);
                //await _context.SaveChangesAsync();

                //Procesa Errores
                foreach (var error in resultadopersona.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                
            }
            return View(persona);
        }

        // GET: Personas1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Dni,Email")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
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
            return View(persona);
        }

        // GET: Personas1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personas == null)
            {
                return Problem("Entity set 'EstacionamientoContext.Personas'  is null.");
            }
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return (_context.Personas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
