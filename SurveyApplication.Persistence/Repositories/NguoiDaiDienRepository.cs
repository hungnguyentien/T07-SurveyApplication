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
    public class NguoiDaiDienRepository : GenericRepository<NguoiDaiDien>, INguoiDaiDienRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public NguoiDaiDienRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NguoiDaiDien> GetById(Guid id)
        {
            return await _dbContext.NguoiDaiDien.FirstOrDefaultAsync(x => x.MaNguoiDaiDien == id) ?? new NguoiDaiDien();
        }

        public async Task<List<NguoiDaiDien>> GetAll()
        {
            return await _dbContext.NguoiDaiDien.ToListAsync();
        }

        public async Task<NguoiDaiDien> Create(NguoiDaiDien obj)
        {
            obj.ActiveFlag = 1;
            await _dbContext.NguoiDaiDien.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.NguoiDaiDien.FirstOrDefaultAsync(x => x.MaNguoiDaiDien == obj.MaNguoiDaiDien) ?? new NguoiDaiDien();
        }

        public async Task<NguoiDaiDien> Update(NguoiDaiDien obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.NguoiDaiDien.FirstOrDefaultAsync(x => x.MaNguoiDaiDien == obj.MaNguoiDaiDien) ?? new NguoiDaiDien();
        }

        public async Task<NguoiDaiDien> Delete(Guid id)
        {
            var obj = await _dbContext.NguoiDaiDien.FirstOrDefaultAsync(x => x.MaNguoiDaiDien == id) ?? new NguoiDaiDien();
            obj.ActiveFlag = 0;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.NguoiDaiDien.FirstOrDefaultAsync(x => x.MaNguoiDaiDien == obj.MaNguoiDaiDien) ?? new NguoiDaiDien();
        }

        public async Task<bool> ExistsByMaNguoiDaiDien(string MaNguoiDaiDien)
        {
            var entity = await _dbContext.NguoiDaiDien.AsNoTracking().FirstOrDefaultAsync(x => x.MaNguoiDaiDien.ToString() == MaNguoiDaiDien);
            return entity != null;
        }
    }
}
