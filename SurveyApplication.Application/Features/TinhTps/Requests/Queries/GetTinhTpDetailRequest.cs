using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;

namespace SurveyApplication.Application.Features.TinhTps.Requests.Queries;

public class GetTinhTpDetailRequest : IRequest<TinhTpDto>
{
    public int Id { get; set; }
}