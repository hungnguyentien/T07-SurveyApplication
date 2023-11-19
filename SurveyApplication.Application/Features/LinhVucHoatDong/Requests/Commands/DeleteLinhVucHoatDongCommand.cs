using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Commands;

public class DeleteLinhVucHoatDongCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}