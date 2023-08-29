using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Persistence.Repositories
{
    public class NguoiDaiDienRepository : GenericRepository<NguoiDaiDien>, INguoiDaiDienRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public NguoiDaiDienRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByMaNguoiDaiDien(string MaNguoiDaiDien)
        {
            var entity = await _dbContext.NguoiDaiDien.AsNoTracking().FirstOrDefaultAsync(x => x.MaNguoiDaiDien.ToString() == MaNguoiDaiDien);
            return entity != null;
        }
    }
}
