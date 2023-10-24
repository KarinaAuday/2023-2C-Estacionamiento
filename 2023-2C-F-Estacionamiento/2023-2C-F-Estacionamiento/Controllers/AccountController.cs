﻿using _2023_2C_F_Estacionamiento.Data;
using _2023_2C_F_Estacionamiento.Herlpers;
using _2023_2C_F_Estacionamiento.Models;
using _2023_2C_F_Estacionamiento.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SQLitePCL;
using System.Security.Cryptography.X509Certificates;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<Persona> _signinManager;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _rolManager;
        private readonly EstacionamientoContext _context;

        public AccountController(UserManager<Persona> userManager, SignInManager<Persona> signInManager, RoleManager<Rol> rolManager, EstacionamientoContext context)
        {
            this._userManager = userManager;
            this._signinManager = signInManager;
            this._rolManager = rolManager;
            this._context = context;

        }
        //Para que permita registrarse a alguien sin incio de sesion
        [AllowAnonymous]
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
                    //Agrego por default el rol de cliente
                    var resultadoAddRole = await _userManager.AddToRoleAsync(clienteACrear, Configs.ClienteRolName);
                    if (resultadoAddRole.Succeeded)
                    {
                        await _signinManager.SignInAsync(clienteACrear, isPersistent: false);
                        //Redirecciono para llenar los datos del Cliente
                        return RedirectToAction("Edit", "Clientes", new { id = clienteACrear.Id });

                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, $"No se puede agregar el rol {Configs.ClienteRolName}");

                    }

                }

                foreach (var error in resultadoCreacion.Errors)
                {
                    //Muestro los errores al momento de crear usuario

                    ModelState.AddModelError(String.Empty, error.Description);

                }

            }



            return View(viewModel);


        }
        [AllowAnonymous]

        public IActionResult IniciarSesion(string returnUrl)
        {
            //ViewBag y viewData
            TempData["ReturnUrl"] = returnUrl;

            return View();
        }

        [AllowAnonymous]

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login loginViewModel)
        {

            //Retorna a la URL que quise acceder antes de inciar sesion
            if (ModelState.IsValid)
            {
                string returnUrl = TempData["ReturnUrl"] as string;

                // tempData guarda info por fuera del bloque de codigo, o sea a la vista y al proximo return generando una cookie temporal


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

        //Solo puede listar si esta logueado como Admin  
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListarRoles()
        {
            //2 formas de obtener los Roles Exsitentes
            var roles = _rolManager.Roles.ToList();
            var roles2 = _context.Roles.ToList();
            return View(roles);
        }

        public IActionResult AccesoDenegado(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        public IActionResult TestCurrentUser()
        {
            if (_signinManager.IsSignedIn(User))
            {
                string nombreUsuario = User.Identity.Name;
                Persona persona = _context.Personas.FirstOrDefault(p => p.NormalizedEmail == nombreUsuario.ToUpper());
                int personaId = Int32.Parse(_userManager.GetUserId(User));

            }
            return null;
        }
        
    }



}



// retunr url es null si inicia session de una, o tien un dato si es una redireccion
