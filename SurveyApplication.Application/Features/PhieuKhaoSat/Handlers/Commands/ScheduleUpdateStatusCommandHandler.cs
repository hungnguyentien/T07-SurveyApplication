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

            #region Update Bảng khảo sát

            var lstUpdateBks = new List<BangKhaoSat>();
            var hoanThanhKs = await _surveyRepo.BangKhaoSat.GetAllListAsync(x => !x.Deleted && x.TrangThai != (int)EnumBangKhaoSat.TrangThai.HoanThanh);
            foreach (var ks in hoanThanhKs)
            {
                var guiEmails = await _surveyRepo.GuiEmail.GetAllQueryable().Where(x => x.IdBangKhaoSat == ks.Id && !x.Deleted && x.TrangThai == (int)EnumGuiEmail.TrangThai.ThanhCong).Select(x => x.Id).ToListAsync(cancellationToken: cancellationToken);
                var countByKs = await _surveyRepo.KetQua.CountAsync(x => x.TrangThai == (int)EnumKetQua.TrangThai.HoanThanh && !x.Deleted && guiEmails.Contains(x.IdGuiEmail));
                if (guiEmails.Count == countByKs)
                {
                    ks.TrangThai = (int)EnumBangKhaoSat.TrangThai.HoanThanh;
                    lstUpdateBks.Add(ks);
                }
            }

            if (lstUpdateBks.Any())
            {
                await _surveyRepo.BangKhaoSat.UpdateAsync(lstUpdateBks);
                await _surveyRepo.SaveAync();
            }

            #endregion

            #region Updte đợt khảo sát

            //var lstUpdateDks = new List<DotKhaoSat>();
            //var hoanThanhDks2 = await _surveyRepo.DotKhaoSat.GetAllListAsync(x => !x.Deleted && x.TrangThai != (int)EnumDotKhaoSat.TrangThai.HoanThanh);
            //foreach (var dotKhaoSat in hoanThanhDks2)
            //{
            //    var countByDks = await _surveyRepo.BangKhaoSat.CountAsync(x => x.IdDotKhaoSat == dotKhaoSat.Id && !x.Deleted);
            //    var countByTrangThai = await _surveyRepo.BangKhaoSat.CountAsync(x => x.TrangThai == (int)EnumBangKhaoSat.TrangThai.HoanThanh && !x.Deleted);
            //    if (countByDks == countByTrangThai)
            //    {
            //        dotKhaoSat.TrangThai = (int)EnumDotKhaoSat.TrangThai.HoanThanh;
            //        lstUpdateDks.Add(dotKhaoSat);
            //    };
            //}

            //if (!lstUpdateDks.Any())
            //    return new BaseCommandResponse
            //    {
            //        Message = "Cập nhật trạng thái thành công"
            //    };
            //{
            //    await _surveyRepo.DotKhaoSat.UpdateAsync(lstUpdateDks);
            //    await _surveyRepo.SaveAync();
            //}

            #endregion

            return new BaseCommandResponse
            {
                Message = "Cập nhật trạng thái thành công"
            };
        }
    }
}
