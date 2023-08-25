using SurveyApplication.Domain;

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface ICauHoiRepository : IGenericRepository<CauHoi>
    {
        Task<List<Cot>> GetCotByCauHoi(List<int> lstId);
        Task<List<Hang>> GetHangByCauHoi(List<int> lstId);
    }
}
