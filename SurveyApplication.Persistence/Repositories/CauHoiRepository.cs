using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class CauHoiRepository : GenericRepository<CauHoi>, ICauHoiRepository
{
    public CauHoiRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}