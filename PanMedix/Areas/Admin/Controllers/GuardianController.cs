using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PanMedix.Enums;
using PanMedix.Exceptions.Global;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(string userId, GuardianStatus status)
    {
        try
        {
            await _guardianService.ResolveGuardianStatusAsync(userId, status);
        }
        catch (EntityNotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index");
    }
}