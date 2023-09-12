using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.DTOs.TinhTp.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Commands
{
    public class UpdateTinhTpCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateTinhTpCommand, Unit>
    {
        private readonly IMapper _mapper;
        public UpdateTinhTpCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTinhTpCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTinhTpDtoValidator(_surveyRepo.TinhTp);
            var validatorResult = await validator.ValidateAsync(request.TinhTpDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var TinhTp = await _surveyRepo.TinhTp.GetById(request.TinhTpDto?.Id ?? 0);
            _mapper.Map(request.TinhTpDto, TinhTp);
            await _surveyRepo.TinhTp.Update(TinhTp);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
