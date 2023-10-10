using _2023_2C_F_Estacionamiento.Models;
using _2023_2C_F_Estacionamiento.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Persona> _signinManager;
        private readonly UserManager<Persona> _userManager;

        public AccountController(UserManager<Persona> userManager , SignInManager<Persona> signInManager)
        {
            this._userManager = userManager;
            this._signinManager = signInManager;
        }
        public async Task<IActionResult> Registrar([Bind("Email,Password,ConfirmacionPassword")] RegistrarUsuario viewModel)
        {
            if (ModelState.IsValid)
            {
                Cliente clienteACrear = new Cliente();
                {
                    clienteACrear.Email = viewModel.Email;
                    clienteACrear.UserName = viewModel.Email;
                }
                //En este caso si se usar el metodo create asyn 
                //Esto me devuelve un identity Result ,  un resultado booleano
                //Agrego la password , y el create se encarga de tomar este string y hacer el hashing
                var resultadoCreacion = await _userManager.CreateAsync(clienteACrear, viewModel.Password);

                if (resultadoCreacion.Succeeded)
                {

                    await _signinManager.SignInAsync(clienteACrear, isPersistent: false);
                    //Redirecciono para llenar los datos del Cliente
                    return RedirectToAction("Edit", "Clientes", new { id = clienteACrear.Id });
                }

                foreach (var error in resultadoCreacion.Errors)
                {
                    //Muestro los errores al momento de crear usuario

                    ModelState.AddModelError(String.Empty, error.Description);

                }

            }
                


                return View(viewModel);


            }





        }


            
    }



    // retunr url es null si inicia session de una, o tien un dato si es una redireccion
