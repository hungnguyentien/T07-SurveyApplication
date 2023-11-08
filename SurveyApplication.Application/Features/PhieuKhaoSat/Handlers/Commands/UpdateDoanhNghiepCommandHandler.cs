using MediatR;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class UpdateDoanhNghiepCommandHandler : BaseMasterFeatures, IRequestHandler<UpdateDoanhNghiepCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private EmailSettings EmailSettings { get; }

        public UpdateDoanhNghiepCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper,
            IOptions<EmailSettings> emailSettings) : base(surveyRepository)
        {
            _mapper = mapper;
            EmailSettings = emailSettings.Value;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDoanhNghiepCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var thongTinChung =
                JsonConvert.DeserializeObject<EmailThongTinChungDto>(
                    StringUltils.DecryptWithKey(request.UpdateDoanhNghiepDto?.IdGuiEmail, EmailSettings.SecretKey));
            var guiEmail = await _surveyRepo.GuiEmail.GetById(thongTinChung?.IdGuiEmail ?? 0);
            if (guiEmail.Deleted)
                throw new ValidationException("Email này không còn tồn tại");

            if (guiEmail.TrangThai == (int)EnumGuiEmail.TrangThai.ThuHoi)
                throw new ValidationException("Email khảo sát này bị thu hồi");

            var donVi = await _surveyRepo.DonVi.FirstOrDefaultAsync(x => !x.Deleted && x.Id == guiEmail.IdDonVi);
            var nguoiDaiDien = await _surveyRepo.NguoiDaiDien.FirstOrDefaultAsync(x => !x.Deleted && x.Id == guiEmail.IdDonVi);
            if (donVi != null && nguoiDaiDien != null && request.UpdateDoanhNghiepDto != null)
            {
                _mapper.Map(request.UpdateDoanhNghiepDto.DonVi, donVi);
                await _surveyRepo.DonVi.Update(donVi);
                _mapper.Map(request.UpdateDoanhNghiepDto.NguoiDaiDien, nguoiDaiDien);
                await _surveyRepo.NguoiDaiDien.Update(nguoiDaiDien);
                await _surveyRepo.SaveAync();
                response.Message = "Cập nhật thành công!";
                return response;
            }

            response.Success = false;
            response.Message = "Cập nhật không thành công!";
            return response;
        }
    }
}
