using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<bool> ExistsByMaDotKhaoSat(string MaDotKhaoSat)
        {
            var entity = await _dbContext.DotKhaoSat.AsNoTracking().FirstOrDefaultAsync(x => x.MaDotKhaoSat == MaDotKhaoSat);
            return entity != null;
        }

        public async Task<PageCommandResponse<DotKhaoSatDto>> GetByCondition<TOrderBy>(int pageIndex, int pageSize, string keyword, Expression<Func<DotKhaoSatDto, bool>> conditions, Expression<Func<DotKhaoSatDto, TOrderBy>> orderBy)
        {
            var query = from d in _dbContext.DotKhaoSat
                        join b in _dbContext.LoaiHinhDonVi
                        on d.MaLoaiHinh equals b.Id

                        where d.MaDotKhaoSat.Contains(keyword) || d.TenDotKhaoSat.Contains(keyword) ||
                            b.TenLoaiHinh.Contains(keyword)
                        select new DotKhaoSatDto
                        {
                            MaDotKhaoSat = d.MaDotKhaoSat,
                            TenDotKhaoSat = d.TenDotKhaoSat,
                            MaLoaiHinh = b.MaLoaiHinh,
                            TenLoaiHinh = b.TenLoaiHinh,
                            NgayBatDau = d.NgayBatDau,
                            NgayKetThuuc = d.NgayKetThuuc,
                            TrangThai = d.TrangThai,
                        };
            var totalCount = await query.CountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pageResults = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var response = new PageCommandResponse<DotKhaoSatDto>
            {
                PageSize = pageSize,
                PageCount = pageCount,
                PageIndex = pageIndex,
                Data = pageResults,
            };

            return response;
        }
    }
}
