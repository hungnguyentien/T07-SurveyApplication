using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories
{
    public class StgFileRepository : GenericRepository<StgFile>, IStgFileRepository
    {
        public StgFileRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
    }
}
