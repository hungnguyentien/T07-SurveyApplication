namespace SurveyApplication.Domain.Interfaces.Persistence;

public interface IModuleRepository : IGenericRepository<Module>
{
    Task<bool> ExistsByName(string tenModule);
}