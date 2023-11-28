namespace SurveyApplication.Domain.Interfaces.Persistence
{
    public interface ILinhVucHoatDongRepository : IGenericRepository<LinhVucHoatDong>
    {
        Task<bool> ExistsByMaLinhVuc(string maLinhVuc);
    }
}
