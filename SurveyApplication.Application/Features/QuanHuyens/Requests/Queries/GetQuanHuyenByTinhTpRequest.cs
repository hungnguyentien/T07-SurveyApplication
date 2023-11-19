using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;

public class GetQuanHuyenByTinhTpRequest : IRequest<List<QuanHuyenDto>>
{
    public string Id { get; set; }
}