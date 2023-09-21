using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain.Interfaces.Persistence
{
    public interface IModuleRepository : IGenericRepository<Module>
    {
        Task<bool> ExistsByName(string tenModule);
    }
}
