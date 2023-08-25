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
            return await _dbContext.LoaiHinhDonVi.FirstOrDefaultAsync(x => x.MaLoaiHinh == id) ?? new LoaiHinhDonVi();
        }

        public async Task<List<LoaiHinhDonVi>> GetAll()
        {
            return await _dbContext.LoaiHinhDonVi.ToListAsync();
        }

        public async Task<LoaiHinhDonVi> Create(LoaiHinhDonVi obj)
        {
            obj.ActiveFlag = 1;
            await _dbContext.LoaiHinhDonVi.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.LoaiHinhDonVi.FirstOrDefaultAsync(x => x.MaLoaiHinh == obj.MaLoaiHinh) ?? new LoaiHinhDonVi();
        }

        public async Task<LoaiHinhDonVi> Update(LoaiHinhDonVi obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.LoaiHinhDonVi.FirstOrDefaultAsync(x => x.MaLoaiHinh == obj.MaLoaiHinh) ?? new LoaiHinhDonVi();
        }

        public async Task<LoaiHinhDonVi> Delete(string id)
        {
            var obj = await _dbContext.LoaiHinhDonVi.FirstOrDefaultAsync(x => x.MaLoaiHinh == id) ?? new LoaiHinhDonVi();
            obj.ActiveFlag = 0;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.LoaiHinhDonVi.FirstOrDefaultAsync(x => x.MaLoaiHinh == obj.MaLoaiHinh) ?? new LoaiHinhDonVi();
        }

        public async Task<bool> ExistsByMaLoaiHinh(string MaLoaiHinh)
        {
            var entity = await _dbContext.LoaiHinhDonVi.AsNoTracking().FirstOrDefaultAsync(x => x.MaLoaiHinh == MaLoaiHinh);
            return entity != null;
        }
    }
}
