using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class ReleaseHistoryRepository : GenericRepository<ReleaseHistory>, IReleaseHistoryRepository
{
    public ReleaseHistoryRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}