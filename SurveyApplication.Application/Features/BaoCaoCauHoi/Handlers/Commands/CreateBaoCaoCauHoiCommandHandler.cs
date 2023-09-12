using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Commands
{
    public class CreateBaoCaoCauHoiCommandHandler : BaseMasterFeatures, IRequestHandler<CreateBaoCaoCauHoiCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        public CreateBaoCaoCauHoiCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBaoCaoCauHoiCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            await _surveyRepo.BaoCaoCauHoi.InsertAsync(_mapper.Map<List<Domain.BaoCaoCauHoi>>(request.LstBaoCaoCauHoi));
            await _surveyRepo.SaveAync();
            return response;
        }
    }
}
