using MediatR;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;

public class DeleteLoaiHinhDonViCommand : IRequest
{
    public int Id { get; set; }
}