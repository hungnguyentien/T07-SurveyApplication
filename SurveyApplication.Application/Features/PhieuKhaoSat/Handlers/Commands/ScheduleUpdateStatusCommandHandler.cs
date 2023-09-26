using AutoMapper.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class ScheduleUpdateStatusCommandHandler : BaseMasterFeatures, IRequestHandler<ScheduleUpdateStatusCommand, BaseCommandResponse>
    {
        public ScheduleUpdateStatusCommandHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<BaseCommandResponse> Handle(ScheduleUpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var hoanThanhDks = await _surveyRepo.DotKhaoSat.GetAllListAsync(x => !x.Deleted && x.NgayKetThuc.Date.AddDays(1) <= DateTime.Now.Date && x.TrangThai != (int)EnumDotKhaoSat.TrangThai.HoanThanh);
            if (hoanThanhDks.Any())
            {
                hoanThanhDks.ForAll(x => x.TrangThai = (int)EnumDotKhaoSat.TrangThai.HoanThanh);
                await _surveyRepo.DotKhaoSat.UpdateAsync(hoanThanhDks);
                await _surveyRepo.SaveAync();
            }

            var hoanThanhBks = await _surveyRepo.BangKhaoSat.GetAllListAsync(x => !x.Deleted && x.NgayKetThuc.Date.AddDays(1) <= DateTime.Now.Date && x.TrangThai != (int)EnumBangKhaoSat.TrangThai.HoanThanh);
            if (hoanThanhBks.Any())
            {
                hoanThanhBks.ForAll(x => x.TrangThai = (int)EnumBangKhaoSat.TrangThai.HoanThanh);
                await _surveyRepo.BangKhaoSat.UpdateAsync(hoanThanhBks);
                await _surveyRepo.SaveAync();
            }

            var dangKhaoSatDks = await (from a in _surveyRepo.DotKhaoSat.GetAllQueryable()
                                        join b in _surveyRepo.BangKhaoSat.GetAllQueryable() on a.Id equals b.IdDotKhaoSat
                                        where !a.Deleted && !b.Deleted && a.TrangThai == (int)EnumDotKhaoSat.TrangThai.ChoKhaoSat
                                        select a).ToListAsync(cancellationToken: cancellationToken);
            if (dangKhaoSatDks.Any())
            {
                dangKhaoSatDks.ForAll(x => x.TrangThai = (int)EnumDotKhaoSat.TrangThai.DangKhaoSat);
                await _surveyRepo.DotKhaoSat.UpdateAsync(dangKhaoSatDks);
                await _surveyRepo.SaveAync();
            }

            var lstUpdateDks = new List<DotKhaoSat>();
            foreach (var dotKhaoSat in hoanThanhDks)
            {
                var countByDks = await _surveyRepo.BangKhaoSat.CountAsync(x => x.IdDotKhaoSat == dotKhaoSat.Id && !x.Deleted);
                var countByTrangThai = await _surveyRepo.BangKhaoSat.CountAsync(x => x.TrangThai == (int)EnumBangKhaoSat.TrangThai.HoanThanh && !x.Deleted);
                if (countByDks == countByTrangThai) lstUpdateDks.Add(dotKhaoSat);
            }

            if (!lstUpdateDks.Any())
                return new BaseCommandResponse
                {
                    Message = "Cập nhật trạng thái thành công"
                };
            {
                lstUpdateDks.ForAll(x => x.TrangThai = (int)EnumDotKhaoSat.TrangThai.HoanThanh);
                await _surveyRepo.DotKhaoSat.UpdateAsync(lstUpdateDks);
                await _surveyRepo.SaveAync();
            }

            return new BaseCommandResponse
            {
                Message = "Cập nhật trạng thái thành công"
            };
        }
    }
}
