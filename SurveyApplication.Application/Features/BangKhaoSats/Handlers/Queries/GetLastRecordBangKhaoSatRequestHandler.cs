using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.LogUtils;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries;

public class GetLastRecordBangKhaoSatRequestHandler : BaseMasterFeatures,
    IRequestHandler<GetLastRecordBangKhaoSatRequest, string>
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public GetLastRecordBangKhaoSatRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper,
        ILoggerManager logger) : base(
        surveyRepository)
    {
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<string> Handle(GetLastRecordBangKhaoSatRequest request, CancellationToken cancellationToken)
    {
        var lastEntity = await _surveyRepo.BangKhaoSat.GetAllQueryable().OrderByDescending(e => e.Id)
            .FirstOrDefaultAsync(cancellationToken);
        try
        {
            if (lastEntity != null)
            {
                var prefix = lastEntity.MaBangKhaoSat.Substring(0, 3);
                var currentNumber = int.Parse(lastEntity.MaBangKhaoSat.Substring(3));

                currentNumber++;
                var newNumber = currentNumber.ToString("D3");

                return prefix + newNumber;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex);
        }

        return "BKS001";
    }
}