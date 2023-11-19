using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Queries;

public class GetQuanHuyenListRequest : IRequest<List<QuanHuyenDto>>
{
}