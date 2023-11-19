using MediatR;

namespace SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;

public class GetLastRecordBangKhaoSatRequest : IRequest<string>
{
    public string? MaLoaiHinh { get; set; }
}