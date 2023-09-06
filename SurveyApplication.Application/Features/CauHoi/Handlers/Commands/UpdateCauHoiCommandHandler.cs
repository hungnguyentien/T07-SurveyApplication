using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.CauHoi.Validators;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Commands
{
    public class UpdateCauHoiCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateCauHoiCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        public UpdateCauHoiCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateCauHoiCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateCauHoiDtoValidator(_surveyRepo, request.CauHoiDto.Id);
            var validatorResult = await validator.ValidateAsync(request.CauHoiDto, cancellationToken);
            if (!validatorResult.IsValid)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var cauHoi = await _surveyRepo.CauHoi.GetById(request.CauHoiDto.Id);
            _mapper.Map(request.CauHoiDto, cauHoi);
            await _surveyRepo.CauHoi.UpdateAsync(cauHoi);
            await _surveyRepo.SaveAync();
            response.Message = "Cập nhật thành công!";
            response.Id = cauHoi.Id;
            return response;
        }
    }
}
