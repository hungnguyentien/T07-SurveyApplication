using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.BangKhaoSat.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Commands;

public class CreateBangKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<CreateBangKhaoSatCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public CreateBangKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateBangKhaoSatCommand request, CancellationToken cancellationToken)
    {
        if (request.BangKhaoSatDto.NgayBatDau.Date == request.BangKhaoSatDto.NgayKetThuc.Date)
        {
            request.BangKhaoSatDto.NgayBatDau = request.BangKhaoSatDto.NgayBatDau.Date;
            request.BangKhaoSatDto.NgayKetThuc = request.BangKhaoSatDto.NgayKetThuc.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
        var response = new BaseCommandResponse();
        var validator = new CreateBangKhaoSatDtoValidator(_surveyRepo.BangKhaoSat);
        if (request.BangKhaoSatDto != null)
        {
            var validatorResult = await validator.ValidateAsync(request.BangKhaoSatDto, cancellationToken);
            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Tạo mới thất bại";
                response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }
        }

        var dotKhaoSat = await _surveyRepo.DotKhaoSat.GetById(request?.BangKhaoSatDto?.IdDotKhaoSat ?? 0);
        if (dotKhaoSat.TrangThai == (int)EnumDotKhaoSat.TrangThai.HoanThanh)
            throw new FluentValidation.ValidationException("Đợt khảo sát đã kết thúc");

        if (dotKhaoSat.NgayBatDau.Date >
            request.BangKhaoSatDto.NgayBatDau.Date)
            throw new FluentValidation.ValidationException("Ngày bắt đầu không được nhỏ hơn ngày bắt đầu đợt khảo sát");

        if (dotKhaoSat.NgayKetThuc.Date <
            request.BangKhaoSatDto.NgayKetThuc.Date)
            throw new FluentValidation.ValidationException("Ngày kết thúc không được lớn hơn ngày kết thúc đợt khảo sát");

        //TODO Gửi mail mới update trạng thái
        //if (dotKhaoSat.TrangThai == (int)EnumDotKhaoSat.TrangThai.ChoKhaoSat)
        //{
        //    dotKhaoSat.TrangThai = (int)EnumDotKhaoSat.TrangThai.DangKhaoSat;
        //    await _surveyRepo.DotKhaoSat.UpdateAsync(dotKhaoSat);
        //}

        var bangKhaoSat = _mapper.Map<BangKhaoSat>(request.BangKhaoSatDto);
        bangKhaoSat.TrangThai = (int)EnumBangKhaoSat.TrangThai.ChoKhaoSat;
        await _surveyRepo.BangKhaoSat.Create(bangKhaoSat);
        await _surveyRepo.SaveAync();
        if (request.BangKhaoSatDto?.BangKhaoSatCauHoi != null)
        {
            if (request.BangKhaoSatDto?.BangKhaoSatCauHoi.Count == 0)
            {
                request.BangKhaoSatDto.BangKhaoSatCauHoiGroup?.ForEach(x =>
                    {
                        x.BangKhaoSatCauHoi?.ForEach(bks =>
                        {
                            bks.PanelTitle = x.PanelTitle;
                            request.BangKhaoSatDto.BangKhaoSatCauHoi.Add(bks);
                        });
                    });
            }

            var lstBangKhaoSatCauHoi = _mapper.Map<List<BangKhaoSatCauHoi>>(request.BangKhaoSatDto?.BangKhaoSatCauHoi);
            lstBangKhaoSatCauHoi.ForEach(x =>
            {
                x.IdBangKhaoSat = bangKhaoSat.Id; x.Priority =
                    lstBangKhaoSatCauHoi.FindIndex(iBks => iBks == x);
            });
            await _surveyRepo.BangKhaoSatCauHoi.Creates(lstBangKhaoSatCauHoi);
            await _surveyRepo.SaveAync();
        }

        response.Success = true;
        response.Message = "Tạo mới thành công";
        response.Id = bangKhaoSat.Id;
        return response;
    }
}