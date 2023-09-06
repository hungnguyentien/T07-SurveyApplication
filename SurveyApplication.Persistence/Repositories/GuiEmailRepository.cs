using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories
{

    public class GuiEmailRepository : GenericRepository<GuiEmail>, IGuiEmailRepository
    {
        private readonly IConfiguration _configuration;
        public GuiEmailRepository(SurveyApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            DbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<bool> ExistsByMaGuiEmail(string maGuiEmail)
        {
            var entity = await DbContext.GuiEmail.AsNoTracking().FirstOrDefaultAsync(x => x.MaGuiEmail == maGuiEmail);
            return entity != null;
        }
    }
}
