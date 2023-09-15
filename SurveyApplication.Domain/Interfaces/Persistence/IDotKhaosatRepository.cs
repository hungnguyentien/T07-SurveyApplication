namespace SurveyApplication.Domain.Interfaces.Persistence;

public interface IDotKhaoSatRepository : IGenericRepository<DotKhaoSat>
{
    Task<bool> ExistsByMaDotKhaoSat(string maDotKhaoSat);
}