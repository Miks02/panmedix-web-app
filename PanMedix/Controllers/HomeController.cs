using Microsoft.AspNetCore.Mvc;

namespace PanMedix.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        

    }
}
