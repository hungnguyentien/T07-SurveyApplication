using MediatR;
using SurveyApplication.Application.DTOs.StgFile;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;

public class DownloadFileRequest : IRequest<StgFileDto>
{
    public long Id { get; set; }
}