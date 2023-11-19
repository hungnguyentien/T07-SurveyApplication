namespace SurveyApplication.Domain.Interfaces.Persistence;

public interface ITinhTpRepository : IGenericRepository<TinhTp>
{
    Task<bool> ExistsByCode(string code);
}