using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;

public class GetXaPhuongListRequest : IRequest<List<XaPhuongDto>>
{
}