using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class NguoiDaiDienRepository : GenericRepository<NguoiDaiDien>, INguoiDaiDienRepository
{
    private readonly SurveyApplicationDbContext _dbContext;

    public NguoiDaiDienRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByMaNguoiDaiDien(string MaNguoiDaiDien)
    {
        var entity = await _dbContext.NguoiDaiDien.AsNoTracking()
            .FirstOrDefaultAsync(x => x.MaNguoiDaiDien == MaNguoiDaiDien && !x.Deleted);
        return entity != null;
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        var entity = await _dbContext.NguoiDaiDien.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email && !x.Deleted);
        return entity != null;
    }
}