using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;

public class GetQuanHuyenDetailRequest : IRequest<QuanHuyenDto>
{
    public int Id { get; set; }
}