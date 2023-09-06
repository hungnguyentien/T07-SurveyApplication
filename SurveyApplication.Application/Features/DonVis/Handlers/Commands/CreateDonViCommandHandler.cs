using AutoMapper;
using MediatR;
using SurveyApplication.Domain;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.DTOs.DonVi.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands
{
    public class CreateDonViCommandHandler : IRequestHandler<CreateDonViCommand, BaseCommandResponse>
    {
        private readonly IDonViRepository _donViRepository;
        private readonly IMapper _mapper;

        public CreateDonViCommandHandler(IDonViRepository donViRepository, IMapper mapper)
        {
            _donViRepository = donViRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDonViCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDonViDtoValidator(_donViRepository);
            var validatorResult = await validator.ValidateAsync(request.DonViDto);
            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }

            var donVi = _mapper.Map<DonVi>(request.DonViDto);
            donVi.MaDonVi = Guid.NewGuid().ToString();
            donVi = await _donViRepository.Create(donVi);
            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = donVi.Id;
            return response;
        }
    }
}
