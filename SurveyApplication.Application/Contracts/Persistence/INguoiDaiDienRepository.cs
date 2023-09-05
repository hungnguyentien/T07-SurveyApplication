using SurveyApplication.Domain;

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface INguoiDaiDienRepository : IGenericRepository<NguoiDaiDien>
    {
        Task<bool> ExistsByMaNguoiDaiDien(string maNguoiDaiDien);
        Task<NguoiDaiDien?> GetByIdDonVi(int idDonVi);
    }
}
