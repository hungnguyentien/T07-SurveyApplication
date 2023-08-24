using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.LoaiHinhDonVis.Requests.Commands;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;

namespace SurveyApplication.Application.Features.LoaiHinhDonVis.Handlers.Commands
{
    public class CreateLoaiHinhDonViCommandHandler : IRequestHandler<CreateLoaiHinhDonViCommand, BaseCommandResponse>
    {
        private readonly ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        private readonly IMapper _mapper;

        public CreateLoaiHinhDonViCommandHandler(ILoaiHinhDonViRepository loaiHinhDonViRepository, IMapper mapper)
        {
            _loaiHinhDonViRepository = loaiHinhDonViRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLoaiHinhDonViCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLoaiHinhDonViDtoValidator(_loaiHinhDonViRepository);
            var validationResult = await validator.ValidateAsync(request.LoaiHinhDonViDto ?? new CreateLoaiHinhDonViDto());

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Tạo mới không thành công";
                response.Errors = validationResult.Errors.Select(q=> q.ErrorMessage).ToList();
            }

            var loaiHinhDonVi = _mapper.Map<LoaiHinhDonVi>(request.LoaiHinhDonViDto);
            
            loaiHinhDonVi = await _loaiHinhDonViRepository.Create(loaiHinhDonVi);

            response.Success = true;
            response.Message = "Tạo mới thành công";
            response.Id = loaiHinhDonVi.MaLoaiHinh;

            return response;
        }
    }
}
