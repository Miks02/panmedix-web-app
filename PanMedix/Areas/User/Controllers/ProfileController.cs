using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PanMedix.Areas.User.Controllers;

[Area("User")]
[Authorize(Roles = "User, Guardian")]
public class ProfileController : Controller
{
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}