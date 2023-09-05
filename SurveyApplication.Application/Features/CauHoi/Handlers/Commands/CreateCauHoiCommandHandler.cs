using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Application.Responses;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Commands
{
    public class CreateCauHoiCommandHandler : IRequestHandler<CreateCauHoiCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICauHoiRepository _cauHoiRepository;
        public CreateCauHoiCommandHandler(IMapper mapper, ICauHoiRepository cauHoiRepository)
        {
            _mapper = mapper;
            _cauHoiRepository = cauHoiRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateCauHoiCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            //var validator = new CreateDonViDtoValidator(_donViRepository);
            //var validatorResult = await validator.ValidateAsync(request.DonViDto);

            //if (validatorResult.IsValid == false)
            //{
            //    response.Success = false;
            //    response.Message = "Tạo mới thất bại";
            //    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
            //    throw new ValidationException(validatorResult);
            //}

            var cauHoi = _mapper.Map<Domain.CauHoi>(request.CauHoiDto);
            await _cauHoiRepository.Create(cauHoi);
            response.Success = true;
            response.Message = "Tạo mới thành công!";
            response.Id = cauHoi.Id;
            return response;
        }
    }
}
