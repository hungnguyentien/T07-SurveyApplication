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

        public async Task<LoaiHinhDonVi> GetById(string id)
        {
            return await _dbContext.LoaiHinhDonVis.FirstOrDefaultAsync(x => x.MaLoaiHinh == id) ?? new LoaiHinhDonVi();
        }

        public async Task<List<LoaiHinhDonVi>> GetAll()
        {
            return await _dbContext.LoaiHinhDonVis.ToListAsync();
        }

        public async Task<LoaiHinhDonVi> Create(LoaiHinhDonVi obj)
        {
            await _dbContext.LoaiHinhDonVis.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.LoaiHinhDonVis.FirstOrDefaultAsync(x => x.MaLoaiHinh == obj.MaLoaiHinh) ?? new LoaiHinhDonVi();
        }

        public async Task<LoaiHinhDonVi> Update(LoaiHinhDonVi obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.LoaiHinhDonVis.FirstOrDefaultAsync(x => x.MaLoaiHinh == obj.MaLoaiHinh) ?? new LoaiHinhDonVi();
        }

        public async Task<LoaiHinhDonVi> Delete(string id)
        {
            var obj = await _dbContext.LoaiHinhDonVis.FirstOrDefaultAsync(x => x.MaLoaiHinh == id) ?? new LoaiHinhDonVi();
            obj.ActiveFlag = 0;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.LoaiHinhDonVis.FirstOrDefaultAsync(x => x.MaLoaiHinh == obj.MaLoaiHinh) ?? new LoaiHinhDonVi();
        }

        public async Task<bool> ExistsByMaLoaiHinh(string maloaihinh)
        {
            var entity = await _dbContext.LoaiHinhDonVis.AsNoTracking().FirstOrDefaultAsync(x => x.MaLoaiHinh == maloaihinh);
            return entity != null;
        }
    }
}
