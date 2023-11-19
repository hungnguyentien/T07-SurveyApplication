using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;

public class CreateQuanHuyenCommand : IRequest<BaseCommandResponse>
{
    public CreateQuanHuyenDto? QuanHuyenDto { get; set; }
}