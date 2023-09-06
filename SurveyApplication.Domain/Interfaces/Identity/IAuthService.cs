using SurveyApplication.Domain.Common.Identity;

namespace SurveyApplication.Domain.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
