using AutoMapper;
using FluentValidation;
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
            if (request.CreateKetQuaDto == null)
            {
                response.Success = false;
                response.Message = "Không có kết quả!";
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

            var guiEmail = await _surveyRepo.GuiEmail.GetById(request.CreateKetQuaDto?.IdGuiEmail ?? 0);
            var bks = await _surveyRepo.BangKhaoSat.GetById(guiEmail.IdBangKhaoSat);
            switch (bks.TrangThai)
            {
                case (int)EnumTrangThai.TrangThai.HoanThanh:
                    throw new ValidationException("Bảng khảo sát đã hoàn thành");
                case (int)EnumTrangThai.TrangThai.TamDung:
                    throw new ValidationException("Bảng khảo sát đã tạm dừng");
            }

            var countBks = await _surveyRepo.GuiEmail.CountAsync(x => x.IdBangKhaoSat == guiEmail.IdBangKhaoSat);
            var countKq = await _surveyRepo.KetQua.CountAsync(x => x.IdGuiEmail == guiEmail.Id);
            if (countBks == countKq)
            {
                bks.TrangThai = (int)EnumTrangThai.TrangThai.HoanThanh;
                await _surveyRepo.BangKhaoSat.UpdateAsync(bks);
            }

            var ketQua = _mapper.Map<KetQua>(request.CreateKetQuaDto) ?? new KetQua();
            var kqDb = await _surveyRepo.KetQua.FirstOrDefaultAsync(x => request.CreateKetQuaDto != null && x.IdGuiEmail == request.CreateKetQuaDto.IdGuiEmail && !x.Deleted);
            if (kqDb == null)
                await _surveyRepo.KetQua.Create(ketQua);
            else
                await _surveyRepo.KetQua.UpdateAsync(_mapper.Map(request.CreateKetQuaDto, kqDb));

            await _surveyRepo.SaveAync();
            response.Success = true;
            response.Message = "Gửi thông tin thành công!";
            response.Id = ketQua.Id;
            return response;
        }
    }
}
