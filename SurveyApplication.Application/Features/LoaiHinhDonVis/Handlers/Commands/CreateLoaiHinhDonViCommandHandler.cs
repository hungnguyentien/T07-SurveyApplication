using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands
{
    public class CreateLoaiHinhDonViCommandHandler : IRequestHandler<CreateLoaiHinhDonViCommand, BaseCommandResponse>
    {
        private readonly ILoaiHinhDonViRepository _LoaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public CreateLoaiHinhDonViCommandHandler(ILoaiHinhDonViRepository LoaiHinhDonViRepository, IMapper mapper)
        {
            _LoaiHinhDonViRepository = LoaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLoaiHinhDonViDtoValidator(_LoaiHinhDonViRepository);
            var validatorResult = await validator.ValidateAsync(request.LoaiHinhDonViDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }

            var LoaiHinhDonVi = _mapper.Map<LoaiHinhDonVi>(request.LoaiHinhDonViDto);

            LoaiHinhDonVi = await _LoaiHinhDonViRepository.Create(LoaiHinhDonVi);

            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = LoaiHinhDonVi.Id;
            return response;
        }
    }
}
