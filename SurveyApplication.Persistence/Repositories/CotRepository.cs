using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories
{
    public class CotRepository: GenericRepository<Cot>, ICotRepository
    {
        public CotRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
    }
}
