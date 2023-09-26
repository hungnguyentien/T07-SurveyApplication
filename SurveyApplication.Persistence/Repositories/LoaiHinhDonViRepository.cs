using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class LoaiHinhDonViRepository : GenericRepository<LoaiHinhDonVi>, ILoaiHinhDonViRepository
{
    private readonly SurveyApplicationDbContext _dbContext;

    public LoaiHinhDonViRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByMaLoaiHinh(string MaLoaiHinh)
    {
        var entity = await _dbContext.LoaiHinhDonVi.AsNoTracking().FirstOrDefaultAsync(x => x.MaLoaiHinh == MaLoaiHinh && !x.Deleted);
        return entity != null;
    }

    public async Task<bool> ExistsByName(string tenLoaiHinh)
    {
        var entity = await _dbContext.LoaiHinhDonVi.AsNoTracking().FirstOrDefaultAsync(x => x.TenLoaiHinh == tenLoaiHinh && !x.Deleted);
        return entity != null;
    }
}