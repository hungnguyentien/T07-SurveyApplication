using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            obj.ActiveFlag = 1;
            await _dbContext.LoaiHinhDonVis.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.LoaiHinhDonVis.FirstOrDefaultAsync(x => x.MaLoaiHinh == obj.MaLoaiHinh) ?? new LoaiHinhDonVi();
        }

        public async Task Update(LoaiHinhDonVi obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var obj = await _dbContext.LoaiHinhDonVis.FirstOrDefaultAsync(x => x.MaLoaiHinh == id) ?? new LoaiHinhDonVi();
            obj.ActiveFlag = 0;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(IReadOnlyList<LoaiHinhDonVi> Items, int TotalCount)> Search(Expression<Func<LoaiHinhDonVi, bool>> filter, int pageNumber, int pageSize)
        {
            var query = _dbContext.Set<LoaiHinhDonVi>().Where(filter);
            var totalCount = await query.CountAsync();

            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }
    }
}
