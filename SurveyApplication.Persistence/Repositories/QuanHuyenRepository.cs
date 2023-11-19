using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class QuanHuyenRepository : GenericRepository<QuanHuyen>, IQuanHuyenRepository
{
    private readonly SurveyApplicationDbContext _dbContext;

    public QuanHuyenRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByCode(string code)
    {
        var entity = await _dbContext.QuanHuyen.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code && !x.Deleted);
        return entity != null;
    }
}