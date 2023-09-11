using System.Collections;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.Enums;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using static SurveyApplication.Application.Enums.EnumCommon;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class DongBoBaoCaoCommandHandler : BaseMasterFeatures, IRequestHandler<DongBoBaoCaoCommand, BaseCommandResponse>
    {
        public DongBoBaoCaoCommandHandler(ISurveyRepositoryWrapper surveyRepository) : base(surveyRepository)
        {
        }

        public async Task<BaseCommandResponse> Handle(DongBoBaoCaoCommand request, CancellationToken cancellationToken)
        {
            var lstBaoCaoCauHoi = new List<BaoCaoCauHoiDto>();
            //TODO lấy kết quả chưa quét
            var lstKetQuaHoanThanh = await _surveyRepo.KetQua.GetAllQueryable().Where(x =>
                !x.Deleted && x.TrangThai == (int)EnumKetQua.TrangThai.HoanThanh && x.ActiveFlag != (int)EnumCommon.ActiveFlag.InActive).Select(x => new
                {
                    x.IdGuiEmail,
                    x.DauThoiGian,
                    DataKq = JsonConvert.DeserializeObject<Dictionary<string, object>>(x.Data),
                }).ToListAsync(cancellationToken);

            var lstCauHoi = await _surveyRepo.CauHoi.GetAllListAsync(x => !x.Deleted);
            var lstCauHoiPhu = await _surveyRepo.Hang.GetAllListAsync(x => !x.Deleted);
            var lstDapAn = await _surveyRepo.Cot.GetAllListAsync(x => !x.Deleted);
            //TODO lấy dữ liệu đáp án
            var baoCaoDapAn = from a in lstDapAn
                              join b in lstCauHoi on a.IdCauHoi equals b.Id
                              join c in lstCauHoiPhu on b.Id equals c.IdCauHoi into cjTb
                              from chp in cjTb.DefaultIfEmpty()
                              select new
                              {
                                  IdCauHoi = b.Id,
                                  CauHoi = b.TieuDe,
                                  MaCauHoi = b.MaCauHoi,
                                  MaCauHoiPhu = chp.MaHang,
                                  CauHoiPhu = chp.NoiDung,
                                  MaCauTraLoi = a.MaCot,
                                  LoaiCauHoi = b.LoaiCauHoi
                              };
            //TODO lấy dữ liệu khảo sát ... theo đáp đán (có mã đáp án nhưng chưa có giá trị câu trả lời)
            var baoCaoCauTraLoi = await (from a in _surveyRepo.BangKhaoSatCauHoi.GetAllQueryable().AsNoTracking()
                                         join b in _surveyRepo.BangKhaoSat.GetAllQueryable().AsNoTracking() on a.IdBangKhaoSat equals b.Id
                                         join c in _surveyRepo.DotKhaoSat.GetAllQueryable().AsNoTracking() on b.IdDotKhaoSat equals c.Id
                                         join d in _surveyRepo.GuiEmail.GetAllQueryable().AsNoTracking() on b.Id equals d.IdBangKhaoSat
                                         join e in _surveyRepo.DonVi.GetAllQueryable().AsNoTracking() on d.IdDonVi equals e.Id
                                         join f in _surveyRepo.NguoiDaiDien.GetAllQueryable().AsNoTracking() on e.Id equals f.IdDonVi
                                         join g in baoCaoDapAn on a.IdCauHoi equals g.IdCauHoi
                                         where !a.Deleted && d.TrangThai == (int)EnumGuiEmail.TrangThai.ThanhCong
                                         select new BaoCaoCauTraLoiDto
                                         {
                                             IdGuiEmail = d.Id,
                                             IdBangKhaoSat = a.IdBangKhaoSat,
                                             IdDotKhaoSat = c.Id,
                                             IdLoaiHinhDonVi = c.IdLoaiHinh,
                                             TenDaiDienCq = f.HoTen,
                                             //TODO dữ liệu đáp án
                                             IdCauHoi = g.IdCauHoi,
                                             CauHoi = g.CauHoi,
                                             MaCauHoi = g.MaCauHoi,
                                             MaCauHoiPhu = g.MaCauHoiPhu,
                                             CauHoiPhu = g.CauHoiPhu,
                                             MaCauTraLoi = g.MaCauTraLoi,
                                             LoaiCauHoi = g.LoaiCauHoi
                                         }).ToListAsync(cancellationToken);

            var baoCaoCauHoiKetQua = from a in baoCaoCauTraLoi
                                     join b in lstKetQuaHoanThanh on a.IdGuiEmail equals b.IdGuiEmail
                                     select new BaoCaoCauHoiKetQuaDto
                                     {
                                         IdGuiEmail = a.IdGuiEmail,
                                         IdBangKhaoSat = a.IdBangKhaoSat,
                                         IdDotKhaoSat = a.IdDotKhaoSat,
                                         IdLoaiHinhDonVi = a.IdLoaiHinhDonVi,
                                         TenDaiDienCq = a.TenDaiDienCq,
                                         IdCauHoi = a.IdCauHoi,
                                         CauHoi = a.CauHoi,
                                         MaCauHoi = a.MaCauHoi,
                                         MaCauHoiPhu = a.MaCauHoiPhu,
                                         CauHoiPhu = a.CauHoiPhu,
                                         MaCauTraLoi = a.MaCauTraLoi,
                                         LoaiCauHoi = a.LoaiCauHoi,
                                         //TODO dữ liệu kết quả
                                         DauThoiGian = b.DauThoiGian,
                                         DataKq = b.DataKq
                                     };

            baoCaoCauHoiKetQua.ToList().ForEach(x =>
            {
                var baoCao = new BaoCaoCauHoiDto
                {
                    IdBangKhaoSat = x.IdBangKhaoSat,
                    IdDotKhaoSat = x.IdDotKhaoSat,
                    IdLoaiHinhDonVi = x.IdLoaiHinhDonVi,
                    TenDaiDienCq = x.TenDaiDienCq,
                    IdCauHoi = x.IdCauHoi,
                    CauHoi = x.CauHoi,
                    MaCauHoi = x.MaCauHoi,
                    MaCauHoiPhu = x.MaCauHoiPhu,
                    CauHoiPhu = x.CauHoiPhu,
                    MaCauTraLoi = x.MaCauTraLoi,
                    LoaiCauHoi = x.LoaiCauHoi,
                    DauThoiGian = x.DauThoiGian,

                    CauTraLoi = ""
                };
                var dataCauTraLoi = x.DataKq?.GetValueOrDefault(x.MaCauHoi)?.ToString() ?? "";
                if (x.LoaiCauHoi == (int)EnumCauHoi.Type.Radio || x.LoaiCauHoi == (int)EnumCauHoi.Type.Text || x.LoaiCauHoi == (int)EnumCauHoi.Type.LongText)
                {
                    baoCao.CauTraLoi = dataCauTraLoi;
                    lstBaoCaoCauHoi.Add(baoCao);
                }
                else if (x.LoaiCauHoi == (int)EnumCauHoi.Type.CheckBox)
                {
                    var lstCauTraLoi = JsonConvert.DeserializeObject<List<string>>(dataCauTraLoi);
                    lstCauTraLoi?.ForEach(ctl =>
                    {
                        var baoCaoNew = Utility.Ultils.DeepCopy(baoCao);
                        baoCaoNew.CauTraLoi = ctl;
                        lstBaoCaoCauHoi.Add(baoCaoNew);
                    });
                }else if (x.LoaiCauHoi == (int)EnumCauHoi.Type.Text)
                {

                }


            });


            var response = new BaseCommandResponse();
            return response;
        }
    }
}
