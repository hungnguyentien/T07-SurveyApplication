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

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface IDotKhaoSatRepository :IGenericRepository<DotKhaoSat>
    {
        Task<bool> ExistsByMaDotKhaoSat(string maDotKhaoSat);
        Task<PageCommandResponse<DotKhaoSatDto>> GetByCondition<TOrderBy>(int pageIndex, int pageSize, string keyword, Expression<Func<DotKhaoSatDto, bool>> conditions, Expression<Func<DotKhaoSatDto, TOrderBy>> orderBy);
    }
}
