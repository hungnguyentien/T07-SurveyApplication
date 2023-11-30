using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;

namespace SurveyApplication.Persistence.Repositories;
public class LogNhanMailRepository : GenericRepository<LogNhanMail>, ILogNhanMailRepository
{
    public LogNhanMailRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}