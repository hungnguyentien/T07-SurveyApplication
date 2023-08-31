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

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface IDonViRepository : IGenericRepository<DonVi>
    {
        Task<bool> ExistsByMaDonVi(string maDonVi);
        Task<PageCommandResponse<DonViDto>> GetByCondition<TOrderBy>(int pageIndex, int pageSize, Expression<Func<DonViDto, bool>> conditions, Expression<Func<DonViDto, TOrderBy>> orderBy);
    }
}
