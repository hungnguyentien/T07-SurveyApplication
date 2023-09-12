using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.XaPhuong.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Commands
{
    public class UpdateXaPhuongCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateXaPhuongCommand, Unit>
    {
        private readonly IMapper _mapper;

        public UpdateXaPhuongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateXaPhuongCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateXaPhuongDtoValidator(_surveyRepo.XaPhuong);
            var validatorResult = await validator.ValidateAsync(request.XaPhuongDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var XaPhuong = await _surveyRepo.XaPhuong.GetById(request.XaPhuongDto?.Id ?? 0);
            _mapper.Map(request.XaPhuongDto, XaPhuong);
            await _surveyRepo.XaPhuong.Update(XaPhuong);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
