using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class BangKhaoSatCauHoiRepository : GenericRepository<BangKhaoSatCauHoi>, IBangKhaoSatCauHoiRepository
{
    public BangKhaoSatCauHoiRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}