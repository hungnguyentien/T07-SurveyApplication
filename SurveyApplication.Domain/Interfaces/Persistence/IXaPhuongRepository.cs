namespace SurveyApplication.Domain.Interfaces.Persistence;

public interface IXaPhuongRepository : IGenericRepository<XaPhuong>
{
    Task<bool> ExistsByCode(string code);
}