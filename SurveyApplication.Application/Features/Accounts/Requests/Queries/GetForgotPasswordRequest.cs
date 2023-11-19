using System.ComponentModel.DataAnnotations;
using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Accounts.Requests.Queries;

public class GetForgotPasswordRequest : IRequest<BaseCommandResponse>
{
    [Required] [EmailAddress] public string Email { get; set; }
}