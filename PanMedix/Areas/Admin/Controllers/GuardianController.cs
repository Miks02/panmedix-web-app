using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Index(string search, string sort, string filter, int page = 1, int pageSize = 10)
    {
        var pagedResult = await _guardianService.GetPagedGuardiansAsync(page, pageSize, search, filter, sort);
        
        return View(pagedResult);
    }
}