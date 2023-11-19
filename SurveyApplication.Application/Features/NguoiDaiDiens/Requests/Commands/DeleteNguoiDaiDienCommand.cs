using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;

public class DeleteNguoiDaiDienCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}