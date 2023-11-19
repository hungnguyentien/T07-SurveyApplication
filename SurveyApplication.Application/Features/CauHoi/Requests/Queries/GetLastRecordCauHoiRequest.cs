using MediatR;

namespace SurveyApplication.Application.Features.CauHoi.Requests.Queries;

public class GetLastRecordCauHoiRequest : IRequest<string>
{
    public string? MaLoaiHinh { get; set; }
}