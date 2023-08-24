using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface IBangKhaoSatRepository : IGenericRepository<BangKhaoSat>
    {
        Task<bool> ExistsByMaBangKhaoSat(string maBangKhaoSat);
    }
}
