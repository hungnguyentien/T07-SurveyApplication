using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain.Interfaces.Persistence
{
    public interface IQuanHuyenRepository : IGenericRepository<QuanHuyen>
    {
        Task<bool> ExistsByCode(string code);
    }
}
