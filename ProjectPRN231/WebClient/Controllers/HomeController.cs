using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Title"] = "About"; 
            return View(); 
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}

