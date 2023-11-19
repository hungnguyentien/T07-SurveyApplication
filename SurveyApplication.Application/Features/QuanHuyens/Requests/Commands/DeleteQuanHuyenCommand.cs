using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;

public class DeleteQuanHuyenCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}