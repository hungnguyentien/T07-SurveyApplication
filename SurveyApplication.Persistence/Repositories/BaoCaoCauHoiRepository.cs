using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class BaoCaoCauHoiRepository : GenericRepository<BaoCaoCauHoi>, IBaoCaoCauHoiRepository
{
    public BaoCaoCauHoiRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}