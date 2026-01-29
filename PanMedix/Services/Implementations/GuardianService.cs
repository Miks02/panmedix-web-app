using Microsoft.EntityFrameworkCore;
using PanMedix.Data.EntityFramework;
using PanMedix.DTO.Global;
using PanMedix.Enums;
using PanMedix.Models;
using PanMedix.Services.Interfaces;
using PanMedix.ViewModels;

namespace PanMedix.Services.Implementations;

public class GuardianService : IGuardianService
{
    private readonly AppDbContext _context;
    private readonly ILogger<GuardianService> _logger;

    public GuardianService(AppDbContext context, ILogger<GuardianService> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<PagedResult<GuardianViewModel>> GetPagedGuardiansAsync(
        int page, 
        int pageSize, 
        string search,
        GuardianStatus status, 
        string sort = "default"
)
    {
        var guardians = await BuildGuardiansQuery(page, pageSize, search, status, sort )
            .Select(g => new GuardianViewModel()
            {
                Id = g.Id,
                FirstName = g.FirstName,
                LastName = g.LastName,
                Email = g.Email!,
                NumberOfWards = g.Patients.Count,
                Status = g.GuardianStatus
            })
            .ToListAsync();
        
        return new PagedResult<GuardianViewModel>(page, pageSize, guardians);
    }

    public async Task ResolveGuardianStatusAsync(bool isApproved)
    {
        throw new NotImplementedException();
    }

    private IQueryable<User> BuildGuardiansQuery(
        int page, 
        int pageSize, 
        string search,
        GuardianStatus? status,
        string sort)
    {
        var query = _context.Users
            .AsNoTracking()
            .OrderBy(w => w.CreatedAt)
            .Where(w => w.GuardianStatus != GuardianStatus.NotGuardian)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            string searchPattern = $"%{search}%";
            query = query.Where(w => EF.Functions.Like(w.FirstName, searchPattern));
        }

        if (status is not null)
        {
            switch (status)
            {
                case GuardianStatus.Approved:
                    query = query.Where(w => w.GuardianStatus == GuardianStatus.Approved);
                    break;
                case GuardianStatus.Pending:
                    query = query.Where(w => w.GuardianStatus == GuardianStatus.Pending);
                    break;
                case GuardianStatus.Denied:
                    query = query.Where(w => w.GuardianStatus == GuardianStatus.Denied);
                    break;
                default: 
                    query = query.Where(w => w.GuardianStatus != GuardianStatus.NotGuardian);
                    break;
            }

        }

        if (!string.IsNullOrEmpty(sort))
        {
            switch (sort)
            {
                case "wards":
                    query = query.OrderByDescending(w => w.Patients.Count);
                    break;
                case "date":
                    query = query.OrderByDescending(w => w.CreatedAt);
                    break;
                default:
                    query = query.OrderByDescending(w => w.FirstName);
                    break;
            }
        }

        query = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        
        return query;

    }
}