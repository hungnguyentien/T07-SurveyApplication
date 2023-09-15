using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;

public class DeleteLoaiHinhDonViCommand : IRequest<BaseCommandResponse>
{
    public List<int> Ids { get; set; }
}