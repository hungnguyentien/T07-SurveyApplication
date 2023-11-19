using MediatR;

namespace SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;

public class GetLastRecordDotKhaoSatRequest : IRequest<string>
{
    public string? MaLoaiHinh { get; set; }
}