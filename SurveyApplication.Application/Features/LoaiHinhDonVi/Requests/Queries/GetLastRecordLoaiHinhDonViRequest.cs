using MediatR;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;

public class GetLastRecordLoaiHinhDonViRequest : IRequest<string>
{
    public string? MaLoaiHinh { get; set; }
}