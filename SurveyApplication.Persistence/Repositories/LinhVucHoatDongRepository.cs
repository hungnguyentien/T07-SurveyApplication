using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class LinhVucHoatDongRepository : GenericRepository<LinhVucHoatDong>, ILinhVucHoatDongRepository
{
    private readonly SurveyApplicationDbContext _dbContext;

    public LinhVucHoatDongRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}