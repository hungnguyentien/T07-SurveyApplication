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

        public async Task<DotKhaoSat> GetById(string id)
        {
            return await _dbContext.DotKhaoSat.FirstOrDefaultAsync(x => x.MaDotKhaoSat == id) ?? new DotKhaoSat();
        }

        public async Task<List<DotKhaoSat>> GetAll()
        {
            return await _dbContext.DotKhaoSat.ToListAsync();
        }

        public async Task<DotKhaoSat> Create(DotKhaoSat obj)
        {
            obj.ActiveFlag = 1;
            await _dbContext.DotKhaoSat.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.DotKhaoSat.FirstOrDefaultAsync(x => x.MaDotKhaoSat == obj.MaDotKhaoSat) ?? new DotKhaoSat();
        }

        public async Task<DotKhaoSat> Update(DotKhaoSat obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.DotKhaoSat.FirstOrDefaultAsync(x => x.MaDotKhaoSat == obj.MaDotKhaoSat) ?? new DotKhaoSat();
        }

        public async Task<DotKhaoSat> Delete(string id)
        {
            var obj = await _dbContext.DotKhaoSat.FirstOrDefaultAsync(x => x.MaDotKhaoSat == id) ?? new DotKhaoSat();
            obj.ActiveFlag = 0;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.DotKhaoSat.FirstOrDefaultAsync(x => x.MaDotKhaoSat == obj.MaDotKhaoSat) ?? new DotKhaoSat();
        }

        public async Task<bool> ExistsByMaDotKhaoSat(string MaDotKhaoSat)
        {
            var entity = await _dbContext.DotKhaoSat.AsNoTracking().FirstOrDefaultAsync(x => x.MaDotKhaoSat == MaDotKhaoSat);
            return entity != null;
        }
    }
}
