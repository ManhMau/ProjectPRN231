using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
