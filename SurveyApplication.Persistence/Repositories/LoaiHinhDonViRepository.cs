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
    public class LoaiHinhDonViRepository : GenericRepository<LoaiHinhDonVi>, ILoaiHinhDonViRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public LoaiHinhDonViRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LoaiHinhDonVi> GetByMaLoaHinh(string maloaihinh)
        {
            return await _dbContext.LoaiHinhDonVis.FirstOrDefaultAsync(x => x.MaLoaiHinh == maloaihinh) ?? new LoaiHinhDonVi();
        }
    }
}
