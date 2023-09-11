using Hangfire.Domain.Interfaces.Hangfire;


namespace Hangfire.Application.Services
{
    public class BaseMasterService
    {
        protected IHangfireRepositoryWrapper _dasHangfireRepo;


        public BaseMasterService(IHangfireRepositoryWrapper dasRepository)
        {
            _dasHangfireRepo = dasRepository;
        }    
    }
}