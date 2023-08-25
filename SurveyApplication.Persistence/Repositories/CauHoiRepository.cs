using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Domain;

namespace SurveyApplication.Persistence.Repositories
{
    public class CauHoiRepository : GenericRepository<CauHoi>, ICauHoiRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public CauHoiRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cot>> GetCotByCauHoi(List<int> lstId)
        {
            var lstCot = await _dbContext.Cot.AsNoTracking().Where(x => lstId.Contains(x.IdCauHoi)).ToListAsync();
            return lstCot;
        }

        public async Task<List<Hang>> GetHangByCauHoi(List<int> lstId)
        {
            var lstHang = await _dbContext.Hang.AsNoTracking().Where(x => lstId.Contains(x.IdCauHoi)).ToListAsync();
            return lstHang;
        }
    }
}
