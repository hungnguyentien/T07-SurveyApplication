using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories
{
    public class KetQuaRepository : GenericRepository<KetQua>, IKetQuaRepository
    {
        public KetQuaRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
        }
    }
}
