using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.DTOs.StgFile;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;

public class UploadFileCommand : IRequest<List<StgFileDto>>
{
    public UploadFileDto? UploadFileDto { get; set; }
}