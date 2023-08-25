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

        public async Task<GuiEmail> GetById(string id)
        {
            return await _dbContext.GuiEmail.FirstOrDefaultAsync(x => x.MaGuiEmail.ToString() == id) ?? new GuiEmail();
        }

        public async Task<List<GuiEmail>> GetAll()
        {
            return await _dbContext.GuiEmail.ToListAsync();
        }

        public async Task<GuiEmail> Create(GuiEmail obj)
        {
            await _dbContext.GuiEmail.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.GuiEmail.FirstOrDefaultAsync(x => x.MaGuiEmail == obj.MaGuiEmail) ?? new GuiEmail();
        }

        public async Task<GuiEmail> Update(GuiEmail obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.GuiEmail.FirstOrDefaultAsync(x => x.MaGuiEmail == obj.MaGuiEmail) ?? new GuiEmail();
        }

        public async Task<GuiEmail> Delete(string id)
        {
            var obj = await _dbContext.GuiEmail.FirstOrDefaultAsync(x => x.MaGuiEmail.ToString() == id) ?? new GuiEmail();
            obj.ActiveFlag = 0;
            await _dbContext.SaveChangesAsync();
            return await _dbContext.GuiEmail.FirstOrDefaultAsync(x => x.MaGuiEmail == obj.MaGuiEmail) ?? new GuiEmail();
        }

        public async Task<bool> ExistsByMaGuiEmail(Guid MaGuiEmail)
        {
            var entity = await _dbContext.GuiEmail.AsNoTracking().FirstOrDefaultAsync(x => x.MaGuiEmail == MaGuiEmail);
            return entity != null;
        }
    }
}
