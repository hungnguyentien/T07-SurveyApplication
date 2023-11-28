using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class BangKhaoSatRepository : GenericRepository<BangKhaoSat>, IBangKhaoSatRepository
{
    public BangKhaoSatRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}