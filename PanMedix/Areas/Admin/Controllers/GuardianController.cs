using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PanMedix.Enums;
using PanMedix.Services.Interfaces;

namespace PanMedix.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class GuardianController : Controller
{
    private readonly IGuardianService _guardianService;
    
    public GuardianController(IGuardianService guardianService)
    {
        _guardianService = guardianService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(
        GuardianStatus status,
        string sort = "name",
        int page = 1, 
        int pageSize = 10
        )
    {
        var pagedResult = await _guardianService.GetPagedGuardiansAsync(page, pageSize, status, sort);

        ViewBag.Sort = sort;
        ViewBag.Status = status;
        
        return View(pagedResult);
    }
}