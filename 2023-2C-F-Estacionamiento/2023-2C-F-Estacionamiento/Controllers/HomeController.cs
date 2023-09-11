using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index2()
        {

            List<string> ciudades = new List<string> { "Roma", "Madrid", "Paris", "Lima" };

            return View(ciudades);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}