using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.LogUtils;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Queries
{
    public class GetLastRecordDotKhaoSatRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordDotKhaoSatRequest, string>
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public GetLastRecordDotKhaoSatRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, ILoggerManager logger) : base(
            surveyRepository)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(GetLastRecordDotKhaoSatRequest request, CancellationToken cancellationToken)
        {
            var lastEntity = await _surveyRepo.DotKhaoSat.GetAllQueryable().OrderByDescending(e => e.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            try
            {
                if (lastEntity != null)
                {
                    var prefix = lastEntity.MaDotKhaoSat.Substring(0, 3);
                    var currentNumber = int.Parse(lastEntity.MaDotKhaoSat.Substring(3));
                    currentNumber++;
                    var newNumber = currentNumber.ToString("D3");
                    return prefix + newNumber;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            

            return "DKS001";
        }
    }
}
