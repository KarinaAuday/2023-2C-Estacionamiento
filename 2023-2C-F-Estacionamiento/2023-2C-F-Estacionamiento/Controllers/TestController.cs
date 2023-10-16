using _2023_2C_F_Estacionamiento.Data;
using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    //Solo usa este controller con Usuario Logueado
    [Authorize]
    public class TestController : Controller
    {
        private readonly EstacionamientoContext _mibase;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _rolmanager;

        public TestController(EstacionamientoContext mibase , UserManager<Persona> userManager, RoleManager<Rol> rolmanager)
        {
            this._mibase = mibase;
            this._userManager = userManager;
            this._rolmanager = rolmanager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChangePassword() 
        {
            //el @User que no esta declarado en ningun lado,  significa la representacion del usuario actual que esta haciendo este Rques
            var persona = await _userManager.FindByNameAsync(@User.Identity.Name);
            if (persona != null) 
            {
                 return View(new ChangePassword() { Id = persona.Id} );
            }
            return View();
        
        }

        [HttpPost]
        public async Task <IActionResult> ChangePassword(ChangePassword model)
        {
           var usuarioActual = await _userManager.FindByNameAsync(@User.Identity.Name);

            if (ModelState.IsValid)
            {
                
                if (!_mibase.Personas.Any(p => p.Id == usuarioActual.Id))
                {
                    return NotFound();
                }
                else
                {

                    var resultado = await _userManager.ChangePasswordAsync(usuarioActual, model.PasswordCurrent, model.PassswordNew);
                        if (resultado.Succeeded)
                    {
                        return RedirectToAction("index", "Home");
                    }
                        else
                    {
                        ModelState.AddModelError(string.Empty, resultado.Errors.First().Code);
                    }
                }


            }


            return View();

        }

    }
}
