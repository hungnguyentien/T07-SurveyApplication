using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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

        public async Task<PageCommandResponse<DonViDto>> GetByConditions<TOrderBy>(int pageIndex, int pageSize, string conditions, Expression<Func<DonViDto, TOrderBy>> orderBy)
        {
            var query = from d in _dbContext.DonVi
                        join b in _dbContext.NguoiDaiDien
                        on d.Id equals b.MaDonVi

                        join o in _dbContext.LoaiHinhDonVi
                        on d.MaLoaiHinh equals o.Id
                        where d.MaDonVi.ToString().Contains(conditions) || d.TenDonVi.Contains(conditions) ||
                            d.DiaChi.Contains(conditions) || b.HoTen.Contains(conditions)
                        select new DonViDto
                        {
                            MaDonVi = d.MaDonVi,
                            TenDonVi = d.TenDonVi,
                            TenLoaiHinh = o.TenLoaiHinh,
                            DiaChi = d.DiaChi,
                            HoTen = b.HoTen,
                        };
            var totalCount = await query.CountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pageResults = await query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

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
