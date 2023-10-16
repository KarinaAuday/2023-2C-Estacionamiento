using _2023_2C_F_Estacionamiento.Models;
using _2023_2C_F_Estacionamiento.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
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

        public IActionResult IniciarSesion(string returnUrl)
        {
            //ViewBag y viewData
            TempData["ReturnUrl"] = returnUrl;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login loginViewModel)
        {
            if (ModelState.IsValid)
            {
                string returnUrl = TempData["ReturnUrl"] as string;

                // tempData guarda info por fuera del bloque de codigo, generando una cookie temporal


                //metodo asincronico para password adato asincronico todo
                //le paso directamente el email (username)
                //recordarme lo defino para ver si defini que sea persistente o no
                var resultado = await _signinManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.Recordarme, false);
                //me devuelve un signinresult
                if (resultado.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                //agrego un errror si no pudo procesar
                ModelState.AddModelError(String.Empty, "Inicio de Sesión inválida");
            }
            return View(loginViewModel);
        }


        public async Task<IActionResult> CerrarSesion()
        {
            //Aca cierro sesion, le dice al browser que elimine esa cookie
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }





    }



}



    // retunr url es null si inicia session de una, o tien un dato si es una redireccion
