using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface IGuiEmailRepository : IGenericRepository<GuiEmail>
    {
        Task<bool> ExistsByMaGuiEmail(Guid maGuiEmail);
    }
}
