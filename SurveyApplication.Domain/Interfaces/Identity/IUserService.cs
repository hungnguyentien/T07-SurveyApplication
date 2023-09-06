using SurveyApplication.Domain.Common.Identity;

namespace SurveyApplication.Domain.Interfaces.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string userId);
    }
}
