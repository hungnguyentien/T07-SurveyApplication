using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Domain.Interfaces.Persistence
{
    public interface IGuiEmailRepository : IGenericRepository<GuiEmail>
    {
        Task<bool> ExistsByMaGuiEmail(string maGuiEmail);
    }
}
