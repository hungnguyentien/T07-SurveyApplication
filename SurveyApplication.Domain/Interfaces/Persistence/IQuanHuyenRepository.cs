namespace SurveyApplication.Domain.Interfaces.Persistence;

public interface IQuanHuyenRepository : IGenericRepository<QuanHuyen>
{
    Task<bool> ExistsByCode(string code);
}