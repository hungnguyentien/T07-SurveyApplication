using MediatR;
using SurveyApplication.Domain.Common.Identity;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries;

public class LoginRequest : IRequest<AuthResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}