namespace SurveyApplication.Domain.Interfaces.Persistence;

public interface INguoiDaiDienRepository : IGenericRepository<NguoiDaiDien>
{
    Task<bool> ExistsByMaNguoiDaiDien(string maNguoiDaiDien);
}