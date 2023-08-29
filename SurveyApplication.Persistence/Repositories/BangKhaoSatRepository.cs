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
    public class BangKhaoSatRepository : GenericRepository<BangKhaoSat>, IBangKhaoSatRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public BangKhaoSatRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByMaBangKhaoSat(string MaBangKhaoSat)
        {
            var entity = await _dbContext.BangKhaoSat.AsNoTracking().FirstOrDefaultAsync(x => x.MaBangKhaoSat == MaBangKhaoSat);
            return entity != null;
        }
    }
}
