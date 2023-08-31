using SurveyApplication.Application.DTOs.BangKhaoSat;
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
    public interface IBangKhaoSatRepository : IGenericRepository<BangKhaoSat>
    {
        Task<bool> ExistsByMaBangKhaoSat(string maBangKhaoSat);
        Task<PageCommandResponse<BangKhaoSatDto>> GetByConditions<TOrderBy>(int pageIndex, int pageSize, string conditions, Expression<Func<BangKhaoSatDto, TOrderBy>> orderBy);
    }
}
