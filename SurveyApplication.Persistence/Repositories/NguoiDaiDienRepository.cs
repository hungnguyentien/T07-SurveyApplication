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

    public class DonViRepository : GenericRepository<DonVi>, IDonViRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public DonViRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DonVi> GetById(Guid id)
        {
            return await _dbContext.DonVis.FirstOrDefaultAsync(x => x.MaDonVi == id) ?? new DonVi();
        }

        public async Task<List<DonVi>> GetAll()
        {
            return await _dbContext.DonVis.ToListAsync();
        }

        public async Task<DonVi> Create(DonVi obj)
        {
            await _dbContext.DonVis.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.DonVis.FirstOrDefaultAsync(x => x.MaDonVi == obj.MaDonVi) ?? new DonVi();
        }

        public async Task<DonVi> Update(DonVi obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.DonVis.FirstOrDefaultAsync(x => x.MaDonVi == obj.MaDonVi) ?? new DonVi();
        }

        public async Task<DonVi> Delete(Guid id)
        {
            var obj = await _dbContext.DonVis.FirstOrDefaultAsync(x => x.MaDonVi == id) ?? new DonVi();
            obj.ActiveFlag = 0;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.DonVis.FirstOrDefaultAsync(x => x.MaDonVi == obj.MaDonVi) ?? new DonVi();
        }

        public async Task<bool> ExistsByMaDonVi(string MaDonVi)
        {
            var entity = await _dbContext.DonVis.AsNoTracking().FirstOrDefaultAsync(x => x.MaDonVi.ToString() == MaDonVi);
            return entity != null;
        }
    }
}
