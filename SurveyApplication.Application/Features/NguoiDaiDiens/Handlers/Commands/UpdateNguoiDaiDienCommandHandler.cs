using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.NguoiDaiDien.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.NguoiDaiDiens.Requests.Commands;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.NguoiDaiDiens.Handlers.Commands
{
    public class UpdateNguoiDaiDienCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateNguoiDaiDienCommand, Unit>
    {
        private readonly IMapper _mapper;

        public UpdateNguoiDaiDienCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateNguoiDaiDienCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateNguoiDaiDienDtoValidator(_surveyRepo.NguoiDaiDien);
            var validatorResult = await validator.ValidateAsync(request.NguoiDaiDienDto);
            if (validatorResult.IsValid == false)
            {
                throw new ValidationException(validatorResult);
            }

            var NguoiDaiDien = await _surveyRepo.NguoiDaiDien.GetById(request.NguoiDaiDienDto?.Id ?? 0);
            _mapper.Map(request.NguoiDaiDienDto, NguoiDaiDien);
            await _surveyRepo.NguoiDaiDien.Update(NguoiDaiDien);
            await _surveyRepo.SaveAync();
            return Unit.Value;
        }
    }
}
