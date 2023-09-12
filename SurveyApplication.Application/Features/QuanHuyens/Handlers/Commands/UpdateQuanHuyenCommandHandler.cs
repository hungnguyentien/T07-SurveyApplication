using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Commands
{
    public class UpdateQuanHuyenCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateQuanHuyenCommand, Unit>
    {
        private readonly IMapper _mapper;

        public UpdateQuanHuyenCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateQuanHuyenCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuanHuyenDtoValidator(_surveyRepo.QuanHuyen);
            var validatorResult = await validator.ValidateAsync(request.QuanHuyenDto);

            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var QuanHuyen = await _surveyRepo.QuanHuyen.GetById(request.QuanHuyenDto?.Id ?? 0);
            _mapper.Map(request.QuanHuyenDto, QuanHuyen);
            await _surveyRepo.QuanHuyen.Update(QuanHuyen);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
