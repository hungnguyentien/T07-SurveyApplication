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
   
    public class DotKhaoSatRepository : GenericRepository<DotKhaoSat>, IDotKhaoSatRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public DotKhaoSatRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByMaDotKhaoSat(string MaDotKhaoSat)
        {
            var entity = await _dbContext.DotKhaoSat.AsNoTracking().FirstOrDefaultAsync(x => x.MaDotKhaoSat == MaDotKhaoSat);
            return entity != null;
        }
    }
}
