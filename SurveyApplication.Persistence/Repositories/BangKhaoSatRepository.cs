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

        public async Task<BangKhaoSat> GetById(string id)
        {
            return await _dbContext.BangKhaoSats.FirstOrDefaultAsync(x => x.MaBangKhaoSat == id) ?? new BangKhaoSat();
        }

        public async Task<List<BangKhaoSat>> GetAll()
        {
            return await _dbContext.BangKhaoSats.ToListAsync();
        }

        public async Task<BangKhaoSat> Create(BangKhaoSat obj)
        {
            await _dbContext.BangKhaoSats.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.BangKhaoSats.FirstOrDefaultAsync(x => x.MaBangKhaoSat == obj.MaBangKhaoSat) ?? new BangKhaoSat();
        }

        public async Task<BangKhaoSat> Update(BangKhaoSat obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.BangKhaoSats.FirstOrDefaultAsync(x => x.MaBangKhaoSat == obj.MaBangKhaoSat) ?? new BangKhaoSat();
        }

        public async Task<BangKhaoSat> Delete(string id)
        {
            var obj = await _dbContext.BangKhaoSats.FirstOrDefaultAsync(x => x.MaBangKhaoSat == id) ?? new BangKhaoSat();
            obj.ActiveFlag = 0;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.BangKhaoSats.FirstOrDefaultAsync(x => x.MaBangKhaoSat == obj.MaBangKhaoSat) ?? new BangKhaoSat();
        }

        public async Task<bool> ExistsByMaBangKhaoSat(string MaBangKhaoSat)
        {
            var entity = await _dbContext.BangKhaoSats.AsNoTracking().FirstOrDefaultAsync(x => x.MaBangKhaoSat == MaBangKhaoSat);
            return entity != null;
        }
    }
}
