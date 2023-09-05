using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.DonVi;
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
    public class BangKhaoSatRepository : GenericRepository<BangKhaoSat>, IBangKhaoSatRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public BangKhaoSatRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByMaBangKhaoSat(string MaBangKhaoSat)
        {
            var entity = await _dbContext.BangKhaoSat.AsNoTracking().FirstOrDefaultAsync(x => x.MaBangKhaoSat == MaBangKhaoSat);
            return entity != null;
        }

        public async Task<PageCommandResponse<BangKhaoSatDto>> GetByCondition<TOrderBy>(int pageIndex, int pageSize, string keyword, Expression<Func<BangKhaoSatDto, bool>> conditions, Expression<Func<BangKhaoSatDto, TOrderBy>> orderBy)
        {
            var query = from d in _dbContext.BangKhaoSat
                        join b in _dbContext.DotKhaoSat
                        on d.MaDotKhaoSat equals b.Id

                        join o in _dbContext.LoaiHinhDonVi
                        on d.MaLoaiHinh equals o.Id

                        join s in _dbContext.GuiEmail
                        on d.Id equals s.MaBangKhaoSat
                        where d.MaBangKhaoSat.Contains(keyword) || d.TenBangKhaoSat.Contains(keyword) ||
                            b.TenDotKhaoSat.Contains(keyword) || o.TenLoaiHinh.Contains(keyword)
                        select new BangKhaoSatDto
                        {
                            MaBangKhaoSat = d.MaBangKhaoSat,
                            TenBangKhaoSat = d.TenBangKhaoSat,
                            MoTa = d.MoTa,
                            NgayBatDau = d.NgayBatDau,
                            NgayKetThuc = d.NgayKetThuc,
                            TrangThai = d.TrangThai,

                            IdBangKhaoSat = d.Id,
                            IdDotKhaoSat = b.Id,

                            MaDotKhaoSat = b.MaDotKhaoSat,
                            TenDotKhaoSat = b.TenDotKhaoSat,

                            MaLoaiHinh = o.MaLoaiHinh,
                            TenLoaiHinh = o.TenLoaiHinh,
                        };
            var totalCount = await query.CountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pageResults = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var response = new PageCommandResponse<BangKhaoSatDto>
            {
                PageSize = pageSize,
                PageCount = totalCount,
                PageIndex = pageIndex,
                Data = pageResults,
            };

            return response;
        }
    }
}
