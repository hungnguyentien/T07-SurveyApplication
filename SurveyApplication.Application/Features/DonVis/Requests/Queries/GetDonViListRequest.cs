using MediatR;
using SurveyApplication.Application.DTOs.DonVi;

namespace SurveyApplication.Application.Features.DonVis.Requests.Queries;

public class GetDonViListRequest : IRequest<List<DonViDto>>
{
}