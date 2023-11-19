using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;

public class UpdateQuanHuyenCommand : IRequest<BaseCommandResponse>
{
    public UpdateQuanHuyenDto? QuanHuyenDto { get; set; }
}