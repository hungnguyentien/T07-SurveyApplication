using MediatR;

namespace SurveyApplication.Application.Features.LinhVucHoatDong.Requests.Queries;

public class GetLastRecordLinhVucRequest : IRequest<string>
{
    public string? MaLinhVuc { get; set; }
}