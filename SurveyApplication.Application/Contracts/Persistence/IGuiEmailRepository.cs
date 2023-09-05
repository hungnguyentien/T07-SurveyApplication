using SurveyApplication.Application.DTOs.GuiEmail;
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
    public interface IGuiEmailRepository : IGenericRepository<GuiEmail>
    {
        Task<bool> ExistsByMaGuiEmail(Guid maGuiEmail);
        Task<PageCommandResponse<GuiEmailDto>> GetByCondition<TOrderBy>(int pageIndex, int pageSize, string keyword, Expression<Func<GuiEmailDto, bool>> conditions, Expression<Func<GuiEmailDto, TOrderBy>> orderBy);
    }
}
