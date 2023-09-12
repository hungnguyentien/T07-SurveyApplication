using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain.Interfaces.Persistence
{
    public interface IXaPhuongRepository : IGenericRepository<XaPhuong>
    {
        Task<bool> ExistsByCode(string code);
    }
}
