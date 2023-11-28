using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class DonViRepository : GenericRepository<DonVi>, IDonViRepository
{
    private readonly SurveyApplicationDbContext _dbContext;

    public DonViRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByMaDonVi(string maDonVi)
    {
        var entity = await _dbContext.DonVi.AsNoTracking().FirstOrDefaultAsync(x => x.MaDonVi == maDonVi && !x.Deleted);
        return entity != null;
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        var entity = await _dbContext.DonVi.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email && !x.Deleted);
        return entity != null;
    }
}