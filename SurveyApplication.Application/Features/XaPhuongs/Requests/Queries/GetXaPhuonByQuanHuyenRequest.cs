using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;

public class GetXaPhuonByQuanHuyenRequest : IRequest<List<XaPhuongDto>>
{
    public string Id { get; set; }
}