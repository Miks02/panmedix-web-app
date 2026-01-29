using PanMedix.DTO.Global;
using PanMedix.Enums;
using PanMedix.ViewModels;

namespace PanMedix.Services.Interfaces;

public interface IGuardianService
{
    Task<GuardianViewModel> GetGuardianByIdAsync(string userId);
    Task<PagedResult<GuardianViewModel>> GetPagedGuardiansAsync(int page, int pageSize, GuardianStatus status, string sort);
    Task ResolveGuardianStatusAsync(string userId, GuardianStatus status);
}