using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Persistence.Repositories
{
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public ModuleRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByName(string tenModule)
        {
            var entity = await _dbContext.Module.AsNoTracking().FirstOrDefaultAsync(x => x.Name == tenModule);
            return entity != null;
        }
    }
}
