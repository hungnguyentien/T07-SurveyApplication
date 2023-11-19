using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong;

namespace SurveyApplication.Application.Features.XaPhuongs.Requests.Queries;

public class GetXaPhuongDetailRequest : IRequest<XaPhuongDto>
{
    public int Id { get; set; }
}