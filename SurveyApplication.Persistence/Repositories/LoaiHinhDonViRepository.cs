using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class LoaiHinhDonViRepository : GenericRepository<LoaiHinhDonVi>, ILoaiHinhDonViRepository
{
    private readonly SurveyApplicationDbContext _dbContext;

    public LoaiHinhDonViRepository(SurveyApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GetLastRecordByMaLoaiHinh()
    {
        var lastEntity = await _dbContext.LoaiHinhDonVi.OrderByDescending(e => e.Id).FirstOrDefaultAsync();

        if (lastEntity != null)
        {
            var prefix = lastEntity.MaLoaiHinh.Substring(0, 2);
            var currentNumber = int.Parse(lastEntity.MaLoaiHinh.Substring(2));

            currentNumber++;
            var newNumber = currentNumber.ToString("D3");

            return prefix + newNumber;
        }

        return "LH001";
    }

    public async Task<bool> ExistsByMaLoaiHinh(string MaLoaiHinh)
    {
        var entity = await _dbContext.LoaiHinhDonVi.AsNoTracking().FirstOrDefaultAsync(x => x.MaLoaiHinh == MaLoaiHinh);
        return entity != null;
    }
}