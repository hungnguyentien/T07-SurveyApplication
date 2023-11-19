using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class JobScheduleRepository : GenericRepository<JobSchedule>, IJobScheduleRepository
{
    public JobScheduleRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}