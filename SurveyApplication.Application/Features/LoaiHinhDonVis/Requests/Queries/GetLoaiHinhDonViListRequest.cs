using MediatR;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Queries;

public class GetLoaiHinhDonViListRequest : IRequest<List<LoaiHinhDonViDto>>
{
}