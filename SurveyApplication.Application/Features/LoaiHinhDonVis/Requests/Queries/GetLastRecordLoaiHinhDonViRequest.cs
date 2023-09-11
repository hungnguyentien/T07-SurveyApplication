using MediatR;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;

public class GetLastRecordLoaiHinhDonViRequest : IRequest<string>
{
    public string? MaLoaiHinh { get; set; }
}