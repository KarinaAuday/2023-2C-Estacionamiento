﻿using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _2023_2C_F_Estacionamiento.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index1(int num)
        {
             num = 111;
            return View(num);
        }

        public IActionResult Index2() {

            List<String> ciudades = new List<string> { "Roma", "Madrid", "Paris", "Lima", "Buenos Aires" };

            return View(ciudades);
        
        
        }


        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}