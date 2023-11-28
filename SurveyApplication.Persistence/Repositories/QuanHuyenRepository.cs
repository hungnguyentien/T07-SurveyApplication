using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SurveyApplication.Persistence.Repositories
{
    public class QuanHuyenRepository : GenericRepository<QuanHuyen>, IQuanHuyenRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public QuanHuyenRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByCode(string code)
        {
            var entity = await _dbContext.QuanHuyen.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code && !x.Deleted);
            return entity != null;
        }
    }
}
