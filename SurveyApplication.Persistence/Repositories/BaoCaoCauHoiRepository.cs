using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;

namespace SurveyApplication.Persistence.Repositories
{
    public class BaoCaoCauHoiRepository : GenericRepository<BaoCaoCauHoi>, IBaoCaoCauHoiRepository
    {
        public BaoCaoCauHoiRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
    }
}
