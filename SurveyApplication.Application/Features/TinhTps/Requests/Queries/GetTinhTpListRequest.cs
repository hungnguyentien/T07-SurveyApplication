using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Queries;

public class GetTinhTpListRequest : IRequest<List<TinhTpDto>>
{
}