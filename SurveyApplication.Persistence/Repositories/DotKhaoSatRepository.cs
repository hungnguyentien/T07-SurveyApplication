using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using System.Linq.Expressions;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

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
