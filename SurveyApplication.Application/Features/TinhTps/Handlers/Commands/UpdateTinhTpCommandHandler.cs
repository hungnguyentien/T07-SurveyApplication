using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.TinhTp.Validators;
using SurveyApplication.Application.DTOs.TinhTp.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Commands
{
    public class UpdateTinhTpCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateTinhTpCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public UpdateTinhTpCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateTinhTpCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateTinhTpDtoValidator(_surveyRepo.TinhTp);
            var validatorResult = await validator.ValidateAsync(request.TinhTpDto, cancellationToken);
            if (!validatorResult.IsValid)
            {
                response.Success = false;
                response.Message = "Cập nhật thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var tinhTp = await _surveyRepo.TinhTp.GetById(request.TinhTpDto?.Id ?? 0);
            _mapper.Map(request.TinhTpDto, tinhTp);
            await _surveyRepo.TinhTp.Update(tinhTp);
            await _surveyRepo.SaveAync();

            response.Message = "Cập nhật thành công!";
            response.Id = tinhTp?.Id ?? 0;
            return response;
        }
    }
}
