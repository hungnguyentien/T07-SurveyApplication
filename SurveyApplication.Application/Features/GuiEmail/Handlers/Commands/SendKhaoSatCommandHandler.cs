using AutoMapper;
using MediatR;
using SurveyApplication.Application.Features.GuiEmail.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Application.DTOs.GuiEmail.Validators;
using SurveyApplication.Application.Enums;

namespace SurveyApplication.Application.Features.GuiEmail.Handlers.Commands
{
    public class SendKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<SendKhaoSatCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public SendKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(SendKhaoSatCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            if (request.GuiEmailDto == null)
                throw new FluentValidation.ValidationException("Gửi email không được để trống");

            var validator = new CreateGuiEmailDtoValidator(_surveyRepo.GuiEmail);
            var validatorResult = await validator.ValidateAsync(request.GuiEmailDto, cancellationToken);
            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            if (await _surveyRepo.BangKhaoSat.AnyAsync(x =>
                    !x.Deleted && request.GuiEmailDto.LstBangKhaoSat.Contains(x.Id) &&
                    (x.NgayBatDau > DateTime.Now || x.NgayKetThuc < DateTime.Now)))
                throw new FluentValidation.ValidationException("Có bảng khảo sát chưa đến hạn hoặc đã hết hạn");

            var lstBangKhaoSat = await _surveyRepo.BangKhaoSat.GetAllListAsync(x =>
                !x.Deleted && x.NgayBatDau < DateTime.Now && x.NgayKetThuc > DateTime.Now &&
                request.GuiEmailDto.LstBangKhaoSat.Contains(x.Id)) ?? throw new FluentValidation.ValidationException("Bảng khảo sát đã hết hạn");
            if (await _surveyRepo.DotKhaoSat.AnyAsync(x =>
                   !x.Deleted && lstBangKhaoSat.Select(b => b.IdDotKhaoSat).Contains(x.Id) &&
                   (x.NgayBatDau > DateTime.Now || x.NgayKetThuuc < DateTime.Now)))
                throw new FluentValidation.ValidationException("Có đợt khảo sát chưa đến hạn hoặc đã hết hạn");

            var guiEmail = _mapper.Map<Domain.GuiEmail>(request.GuiEmailDto) ?? throw new FluentValidation.ValidationException("Gửi email không mapping được");
            var lstDonVi = await _surveyRepo.DonVi.GetAllListAsync(x => request.GuiEmailDto.LstIdDonVi.Contains(x.Id) && !x.Deleted);
            var lstGuiEmail = new List<Domain.GuiEmail>();
            foreach (var bangKhaoSat in lstBangKhaoSat)
            {
                foreach (var donVi in lstDonVi)
                {
                    guiEmail.IdDonVi = donVi.Id;
                    guiEmail.IdBangKhaoSat = bangKhaoSat.Id;
                    guiEmail.MaGuiEmail = Guid.NewGuid().ToString();
                    guiEmail.DiaChiNhan = donVi.Email;
                    guiEmail.TrangThai = (int)EnumGuiEmail.TrangThai.DangGui;
                    lstGuiEmail.Add(guiEmail);
                }
            }


            await _surveyRepo.GuiEmail.InsertAsync(lstGuiEmail);
            await _surveyRepo.SaveAync();
            response.Success = true;
            response.Message = "Tạo mới thành công";
            return response;
        }
    }
}
