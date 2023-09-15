using MediatR;
using SurveyApplication.Domain.Common.Identity;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries;

public class LoginRequest : IRequest<AuthResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}