using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;

public class SendMailCommand : IRequest<EmailRespose>
{
    public int Id { get; set; }
}