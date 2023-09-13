using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;

public class SendMailCommand : IRequest<BaseCommandResponse>
{
    public List<int> LstIdGuiMail { get; set; }
    public bool IsThuHoi { get; set; } = false;
}