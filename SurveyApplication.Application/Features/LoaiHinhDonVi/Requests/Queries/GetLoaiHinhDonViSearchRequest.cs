using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;

public class GetLoaiHinhDonViSearchRequest : IRequest<List<LoaiHinhDonViDto>>
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Keyword { get; set; }
}