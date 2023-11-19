using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class ModuleRepository : GenericRepository<Module>, IModuleRepository
{
    private readonly SurveyApplicationDbContext _dbContext;

    public ModuleRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByName(string tenModule)
    {
        var entity = await _dbContext.Module.AsNoTracking().FirstOrDefaultAsync(x => x.Name == tenModule && !x.Deleted);
        return entity != null;
    }
}