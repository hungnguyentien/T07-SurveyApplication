using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Features.DonVis.Requests.Queries;
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

    public class DonViRepository : GenericRepository<DonVi>, IDonViRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public DonViRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByMaDonVi(string MaDonVi)
        {
            var entity = await _dbContext.DonVi.AsNoTracking().FirstOrDefaultAsync(x => x.MaDonVi.ToString() == MaDonVi);
            return entity != null;
        }

        public async Task<PageCommandResponse<DonViDto>> GetByCondition<TOrderBy>(int pageIndex, int pageSize, string keyword, Expression<Func<DonViDto, bool>> conditions, Expression<Func<DonViDto, TOrderBy>> orderBy)
        {
            var query = from d in _dbContext.DonVi
                        join b in _dbContext.NguoiDaiDien on d.Id equals b.MaDonVi
                        join o in _dbContext.LoaiHinhDonVi on d.MaLoaiHinh equals o.Id
                        where d.MaDonVi.ToString().Contains(keyword) || d.TenDonVi.Contains(keyword) ||
                             d.DiaChi.Contains(keyword) || b.HoTen.Contains(keyword)
                        select new DonViDto
                        {
                            MaDonVi = d.MaDonVi,
                            TenDonVi = d.TenDonVi,
                            MaLoaiHinh = d.MaLoaiHinh,
                            TenLoaiHinh = o.TenLoaiHinh,
                            DiaChi = d.DiaChi,
                            MaNguoiDaiDien = b.MaNguoiDaiDien,
                            HoTen = b.HoTen,
                        };

            var totalCount = await query.CountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pageResults = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var response = new PageCommandResponse<DonViDto>
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
