using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Persistence.Repositories
{
   
    public class GuiEmailRepository : GenericRepository<GuiEmail>, IGuiEmailRepository
    {
        private readonly SurveyApplicationDbContext _dbContext;

        public GuiEmailRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByMaGuiEmail(Guid MaGuiEmail)
        {
            var entity = await _dbContext.GuiEmail.AsNoTracking().FirstOrDefaultAsync(x => x.MaGuiEmail == MaGuiEmail);
            return entity != null;
        }
    }
}
