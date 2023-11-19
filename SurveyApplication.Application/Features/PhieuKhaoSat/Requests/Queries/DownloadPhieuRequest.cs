using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;

public class DownloadPhieuRequest : IRequest<DownloadPhieuDto>
{
    public string Data { get; set; }
}