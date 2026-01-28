using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PanMedix.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class GuardianController : Controller
{
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}