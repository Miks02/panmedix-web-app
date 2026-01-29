using PanMedix.DTO.Global;
using PanMedix.Enums;
using PanMedix.ViewModels;

namespace PanMedix.Services.Interfaces;

public interface IGuardianService
{
    Task<PagedResult<GuardianViewModel>> GetPagedGuardiansAsync(int page, int pageSize, string search, GuardianStatus status, string sort);
    Task ResolveGuardianStatusAsync(bool isApproved);
}