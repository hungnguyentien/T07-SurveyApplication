using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;

namespace SurveyApplication.Persistence.Repositories;

public class AccountRepository : GenericRepository<ApplicationUser>, IAccountRepository
{
    public AccountRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}