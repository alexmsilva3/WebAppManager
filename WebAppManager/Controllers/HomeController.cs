using Microsoft.AspNetCore.Mvc;

namespace WebAppManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }   
    }
}
