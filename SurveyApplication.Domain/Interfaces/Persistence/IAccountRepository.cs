using SurveyApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain.Interfaces.Persistence
{
    public interface IAccountRepository : IGenericRepository<ApplicationUser>
    {
    }
}
