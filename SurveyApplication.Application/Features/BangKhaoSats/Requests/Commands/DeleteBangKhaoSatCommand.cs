using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;

public class DeleteBangKhaoSatCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}