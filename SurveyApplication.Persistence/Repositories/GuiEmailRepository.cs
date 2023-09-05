using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.GuiEmail;
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
   
    public class GuiEmailRepository : GenericRepository<GuiEmail>, IGuiEmailRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public GuiEmailRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByMaGuiEmail(Guid MaGuiEmail)
        {
            var entity = await _dbContext.GuiEmail.AsNoTracking().FirstOrDefaultAsync(x => x.MaGuiEmail == MaGuiEmail);
            return entity != null;
        }

        public async Task<PageCommandResponse<GuiEmailDto>> GetByCondition<TOrderBy>(int pageIndex, int pageSize, string keyword, Expression<Func<GuiEmailDto, bool>> conditions, Expression<Func<GuiEmailDto, TOrderBy>> orderBy)
        {
            var query = from d in _dbContext.GuiEmail
                        join b in _dbContext.BangKhaoSat on d.MaBangKhaoSat equals b.Id
                        where d.MaBangKhaoSat.ToString().Contains(keyword) || b.TenBangKhaoSat.Contains(keyword)
                        select new GuiEmailDto
                        {
                            MaBangKhaoSatEmail = b.MaBangKhaoSat,
                            TenBangKhaoSat = b.TenBangKhaoSat,

                            MaGuiEmail = d.MaGuiEmail,
                            MaBangKhaoSat = d.MaBangKhaoSat,
                            TrangThai = d.TrangThai,
                            ThoiGian = d.ThoiGian,
                        };

            var totalCount = await query.CountAsync();
            var pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

            var pageResults = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var response = new PageCommandResponse<GuiEmailDto>
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
