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
    public class CreateKetQuaCommandHandler : BaseMasterFeatures, IRequestHandler<CreateKetQuaCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        public CreateKetQuaCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateKetQuaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            if (request == null)
            {
                response.Success = false;
                response.Message = "Không tìm thấy kết quả!";
                return response;
            }

            var validator = new CreateKetQuaDtoValidator(_surveyRepo.KetQua);
            var validationResult = await validator.ValidateAsync(request.CreateKetQuaDto ?? new DTOs.PhieuKhaoSat.CreateKetQuaDto());
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Gửi thông tin không thành công!";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var bangKs = await _surveyRepo.BangKhaoSat.GetById(request?.CreateKetQuaDto?.IdBangKhaoSat ?? 0);
            if (bangKs == null)
            {
                response.Success = false;
                response.Message = "Không tồn tại bảng khảo sát!";
                return response;
            }

            bangKs.TrangThai = request?.CreateKetQuaDto?.TrangThai ?? 0;
            await _surveyRepo.BangKhaoSat.Update(bangKs);
            var ketQua = _mapper.Map<KetQua>(request.CreateKetQuaDto) ?? new KetQua();
            var kqDb = await _surveyRepo.KetQua.FirstOrDefaultAsync(x => x.IdBangKhaoSat == request.CreateKetQuaDto.IdBangKhaoSat && x.IdDonVi == request.CreateKetQuaDto.IdDonVi && x.IdNguoiDaiDien == request.CreateKetQuaDto.IdNguoiDaiDien && !x.Deleted);
            if (kqDb == null)
            {
                await _surveyRepo.KetQua.Create(ketQua);
            }
            else
            {
                await _surveyRepo.KetQua.UpdateAsync(_mapper.Map(request.CreateKetQuaDto, kqDb));
            }

            await _surveyRepo.SaveAync();
            response.Success = true;
            response.Message = "Gửi thông tin thành công!";
            response.Id = ketQua.Id;
            return response;
        }
    }
}
