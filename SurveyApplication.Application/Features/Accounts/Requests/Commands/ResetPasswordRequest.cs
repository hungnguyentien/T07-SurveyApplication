using MediatR;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands;

public class ResetPasswordRequest : IRequest<BaseCommandResponse>
{
    public ResetPasswordDto? ResetPasswordDto { get; set; }
}