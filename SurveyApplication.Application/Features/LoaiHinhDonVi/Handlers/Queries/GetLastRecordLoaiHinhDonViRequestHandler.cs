using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Handlers.Queries;

public class GetLastRecordLoaiHinhDonViRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetLastRecordLoaiHinhDonViRequest, string>
{
    private readonly IMapper _mapper;

    public GetLastRecordLoaiHinhDonViRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<string> Handle(GetLastRecordLoaiHinhDonViRequest request, CancellationToken cancellationToken)
    {
        var lastEntity = await _surveyRepo.LoaiHinhDonVi.GetAllQueryable().OrderByDescending(e => e.Id).FirstOrDefaultAsync();

        if (lastEntity != null)
        {
            var prefix = lastEntity.MaLoaiHinh.Substring(0, 2);
            var currentNumber = int.Parse(lastEntity.MaLoaiHinh.Substring(2));

            currentNumber++;
            var newNumber = currentNumber.ToString("D3");

            return prefix + newNumber;
        }

        return "LH001";
    }
}