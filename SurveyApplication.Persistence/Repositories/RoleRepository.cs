using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}