using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class XaPhuongRepository : GenericRepository<XaPhuong>, IXaPhuongRepository
{
    private readonly SurveyApplicationDbContext _dbContext;

    public XaPhuongRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByCode(string code)
    {
        var entity = await _dbContext.XaPhuong.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code && !x.Deleted);
        return entity != null;
    }
}