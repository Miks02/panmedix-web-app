using Microsoft.EntityFrameworkCore;
using PanMedix.Data.EntityFramework;
using PanMedix.DTO.Global;
using PanMedix.Enums;
using PanMedix.Exceptions.Global;
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

    public async Task<GuardianViewModel> GetGuardianByIdAsync(string userId)
    {
        var guardian = await _context.Users
            .AsNoTracking()
            .Where(g => g.Id == userId)
            .Select(g => new GuardianViewModel()
            {
                Id = g.Id,
                FirstName = g.FirstName,
                LastName = g.LastName,
                Email = g.Email!,
                NumberOfWards = g.Wards.Count,
                Status = g.GuardianStatus
            })
            .FirstOrDefaultAsync();

        if (guardian is null)
            throw new EntityNotFoundException("Staratelj nije pronadjen");

        return guardian;
    }

    public async Task<PagedResult<GuardianViewModel>> GetPagedGuardiansAsync(int page,
        int pageSize,
        GuardianStatus status,
        string sort)
    {
        var guardians = await BuildGuardiansQuery(page, pageSize, status, sort )
            .Select(g => new GuardianViewModel()
            {
                Id = g.Id,
                FirstName = g.FirstName,
                LastName = g.LastName,
                Email = g.Email!,
                NumberOfWards = g.Wards.Count,
                Status = g.GuardianStatus
            })
            .ToListAsync();

        var totalGuardians = await _context.Users
            .AsNoTracking()
            .Where(u => u.GuardianStatus != GuardianStatus.NotGuardian)
            .Select(u => u.Id)
            .CountAsync();
        
        return new PagedResult<GuardianViewModel>(page, pageSize, guardians, totalGuardians);
    }

    public async Task ResolveGuardianStatusAsync(string userId, GuardianStatus status)
    {
        var guardian = await _context.Users
            .FirstOrDefaultAsync();
        
        if (guardian is null)
            throw new EntityNotFoundException("Staratelj nije pronadjen");
        
        await _context.Users
            .Where(u => u.GuardianId == userId)
            .ExecuteUpdateAsync(u => u.SetProperty(w => w.GuardianId, (string?)null));
        
        guardian.GuardianStatus = status;
        await _context.SaveChangesAsync();
    }

    private IQueryable<User> BuildGuardiansQuery(
        int page, 
        int pageSize, 
        GuardianStatus? status,
        string sort)
    {
        var query = _context.Users
            .AsNoTracking()
            .OrderBy(w => w.CreatedAt)
            .Where(w => w.GuardianStatus != GuardianStatus.NotGuardian)
            .AsQueryable();
        
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
                    query = query.OrderByDescending(w => w.Wards.Count);
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