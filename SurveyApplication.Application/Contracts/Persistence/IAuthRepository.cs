using SurveyApplication.Application.Features.Auths.Requests.Queries;
using SurveyApplication.Application.Models.Identity;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Contracts.Persistence
{
    public interface IAuthRepository : IGenericRepository<NguoiDung>
    {
        Task<bool> ExistsByMaAuth(string maBangKhaoSat);
        Task<AuthResponse> Login(LoginRequest request);
    }
}
