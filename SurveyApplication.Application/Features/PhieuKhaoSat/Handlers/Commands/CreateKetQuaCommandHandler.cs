using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat.Validators;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class CreateKetQuaCommandHandler : IRequestHandler<CreateKetQuaCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IKetQuaRepository _ketQuaRepository;
        private readonly IBangKhaoSatRepository _bangKhaoSatRepository;
        public CreateKetQuaCommandHandler(IMapper mapper, IKetQuaRepository ketQuaRepository, IBangKhaoSatRepository bangKhaoSatRepository)
        {
            _mapper = mapper;
            _ketQuaRepository = ketQuaRepository;
            _bangKhaoSatRepository = bangKhaoSatRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateKetQuaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateKetQuaDtoValidator(_ketQuaRepository);
            var validationResult = await validator.ValidateAsync(request.CreateKetQuaDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Gửi thông tin không thành công!";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var bangKs = await _bangKhaoSatRepository.GetById(request.CreateKetQuaDto.IdBangKhaoSat);
            if (bangKs == null)
            {
                response.Success = false;
                response.Message = "Không tồn tại bảng khảo sát!";
                return response;
            }

            bangKs.TrangThai = (int)EnumBangKhaoSat.TrangThai.HoanThanh;
            await _bangKhaoSatRepository.Update(bangKs);
            var ketQua = _mapper.Map<KetQua>(request.CreateKetQuaDto) ?? new KetQua();
            ketQua.ActiveFlag = request.CreateKetQuaDto.Status;
            ketQua = await _ketQuaRepository.Create(ketQua);
            response.Success = true;
            response.Message = "Gửi thông tin thành công!";
            response.Id = ketQua.Id;
            return response;
        }
    }
}
