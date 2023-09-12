using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories
{
    public class LinhVucHoatDongRepository : GenericRepository<LinhVucHoatDong>, ILinhVucHoatDongRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;
        public LinhVucHoatDongRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByMaLinhVuc(string maLinhVuc)
        {
            var entity = await _dbContext.LinhVucHoatDong.AsNoTracking().FirstOrDefaultAsync(x => x.MaLinhVuc == maLinhVuc);
            return entity != null;
        }
    }
}
