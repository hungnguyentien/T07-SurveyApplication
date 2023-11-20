using System.Collections;
using System.Globalization;
using FluentValidation;
using GemBox.Document;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Queries;

public class DownloadPhieuRequestHandler : BaseMasterFeatures,
    IRequestHandler<DownloadPhieuRequest, DownloadPhieuDto>
{
    private const string PathDocx = @"TempData\MAU_PKS_DOANH_NGHIEP.docx";
    private const string PathPdf = @"TempData\MAU_PKS_DOANH_NGHIEP.pdf";

    public DownloadPhieuRequestHandler(ISurveyRepositoryWrapper surveyRepository, IOptions<EmailSettings> emailSettings)
        : base(
            surveyRepository)
    {
        EmailSettings = emailSettings.Value;
    }

    private EmailSettings EmailSettings { get; }

    public async Task<DownloadPhieuDto> Handle(DownloadPhieuRequest request,
        CancellationToken cancellationToken)
    {
        var thongTinChung =
            JsonConvert.DeserializeObject<EmailThongTinChungDto>(
                StringUltils.DecryptWithKey(request.Data, EmailSettings.SecretKey));
        var guiEmail = await _surveyRepo.GuiEmail.GetById(thongTinChung?.IdGuiEmail ?? 0);
        if (guiEmail.Deleted)
            throw new ValidationException("Email này không còn tồn tại");

        if (guiEmail.TrangThai == (int)EnumGuiEmail.TrangThai.ThuHoi)
            throw new ValidationException("Email khảo sát này bị thu hồi");

        var bks = await _surveyRepo.BangKhaoSat.FirstOrDefaultAsync(x => !x.Deleted && x.Id == guiEmail.IdBangKhaoSat);
        var donVi = await _surveyRepo.DonVi.FirstOrDefaultAsync(x => !x.Deleted && x.Id == guiEmail.IdDonVi) ??
                    new DonVi();
        var nguoiDaiDien =
            await _surveyRepo.NguoiDaiDien.FirstOrDefaultAsync(x => !x.Deleted && x.Id == guiEmail.IdDonVi) ??
            new NguoiDaiDien();
        var xaPhuong = await _surveyRepo.XaPhuong.FirstOrDefaultAsync(x => !x.Deleted && x.Id == donVi.IdXaPhuong) ??
                       new XaPhuong();
        var quanHuyen = await _surveyRepo.QuanHuyen.FirstOrDefaultAsync(x => !x.Deleted && x.Id == donVi.IdQuanHuyen) ??
                        new QuanHuyen();
        var tinhThanh = await _surveyRepo.TinhTp.FirstOrDefaultAsync(x => !x.Deleted && x.Id == donVi.IdTinhTp) ??
                        new TinhTp();
        var diaChi = new List<string> { donVi.DiaChi ?? "", xaPhuong.Name, quanHuyen.Name, tinhThanh.Name };
        byte[] content;
        //var fileName = $"PKS_{bks?.TenBangKhaoSat}_{donVi.TenDonVi}_MST_{donVi.MaSoThue}.docx";
        var fileName = $"PKS_{bks?.TenBangKhaoSat}_{donVi.TenDonVi}_MST_{donVi.MaSoThue}_{DateTime.Now:dd-MM-yyyy_HH:mm:ss}.pdf";
        var dict = new Dictionary<string, string>
        {
            { "HO_TEN", nguoiDaiDien.HoTen },
            { "CHUC_VU", nguoiDaiDien.ChucVu ?? "" },
            { "DIEN_THOAI", nguoiDaiDien.SoDienThoai },
            { "EMAIL", nguoiDaiDien.Email ?? "" },
            { "TEN_DON_VI", donVi.TenDonVi },
            { "DIA_CHI", string.Join(",", diaChi.Where(x => !string.IsNullOrEmpty(x))) },
            { "DIEN_THOAI_2", donVi.SoDienThoai },
            { "EMAIL_2", donVi.Email },
            //{ "DayOfWeek", DateTime.Now.DayOfWeek.ConvertDayOfWeekToTcvn() },
            { "DayOfWeek", "" },
            { "Day", DateTime.Now.Day.ToString(CultureInfo.InvariantCulture) },
            { "Month", DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) }
        };
        var dictLongText = new Dictionary<string, string>();
        var dictSymbolChar = new Dictionary<string, string>();
        var ketQua = await _surveyRepo.KetQua.FirstOrDefaultAsync(x => !x.Deleted && x.IdGuiEmail == guiEmail.Id);
        if (ketQua != null)
        {
            var lstIdCauHoi = await _surveyRepo.BangKhaoSatCauHoi.GetAllQueryable().AsNoTracking()
                .Where(x => !x.Deleted && x.IdBangKhaoSat == guiEmail.IdBangKhaoSat).Select(x => x.IdCauHoi)
                .ToListAsync(cancellationToken);
            var dictKq = JsonConvert.DeserializeObject<Hashtable>(ketQua.Data);
            var lstCauHoi = await _surveyRepo.CauHoi.GetAllListAsync(x => !x.Deleted && lstIdCauHoi.Contains(x.Id));
            var lstHang = await _surveyRepo.Hang.GetAllQueryable().AsNoTracking()
                .Where(x => !x.Deleted && lstIdCauHoi.Contains(x.IdCauHoi)).ToListAsync(cancellationToken);
            var lstCot = await _surveyRepo.Cot.GetAllQueryable().AsNoTracking()
                .Where(x => !x.Deleted && lstIdCauHoi.Contains(x.IdCauHoi)).ToListAsync(cancellationToken);
            foreach (var cauHoi in lstCauHoi.Where(_ => dictKq != null))
                if (dictKq != null)
                    switch (cauHoi.LoaiCauHoi)
                    {
                        case (int)EnumCauHoi.Type.Radio:
                            {
                                dictSymbolChar.Add(
                                    $"{cauHoi.MaCauHoi}_{dictKq[cauHoi.MaCauHoi]?.ToString().ConvertToCamelString()}",
                                    "F09C");
                                if (!dict.ContainsKey($"{cauHoi.MaCauHoi}_Comment"))
                                    dict.Add($"{cauHoi.MaCauHoi}_Comment",
                                        dictKq[$"{cauHoi.MaCauHoi}-Comment"]?.ToString() ?? "");

                                break;
                            }
                        case (int)EnumCauHoi.Type.CheckBox:
                            {
                                var lstCauTraLoi =
                                    JsonConvert.DeserializeObject<List<string>>(dictKq[cauHoi.MaCauHoi]?.ToString() ??
                                                                                "") ?? new List<string>();
                                foreach (var cauTraLoi in lstCauTraLoi)
                                    dictSymbolChar.Add($"{cauHoi.MaCauHoi}_{cauTraLoi.ConvertToCamelString()}", "F052");

                                if (!dict.ContainsKey($"{cauHoi.MaCauHoi}_Comment"))
                                    dict.Add($"{cauHoi.MaCauHoi}_Comment",
                                        dictKq[$"{cauHoi.MaCauHoi}-Comment"]?.ToString() ?? "");

                                break;
                            }
                        case (int)EnumCauHoi.Type.MultiTextMatrix:
                            {
                                var objCauTraLoi =
                                    JsonConvert.DeserializeObject<Hashtable>(dictKq[cauHoi.MaCauHoi]?.ToString() ?? "");
                                foreach (var hang in lstHang.Where(x => x.IdCauHoi == cauHoi.Id))
                                {
                                    var objCauTraLoiHang =
                                        JsonConvert.DeserializeObject<Hashtable>(objCauTraLoi?[hang.MaHang]?.ToString() ??
                                                                                 "");
                                    foreach (var cot in lstCot.Where(x => x.IdCauHoi == cauHoi.Id))
                                        dict.Add($"{cauHoi.MaCauHoi}_{hang.MaHang}_{cot.MaCot}",
                                            objCauTraLoiHang?[cot.MaCot]?.ToString() ?? "");
                                }

                                break;
                            }
                        case (int)EnumCauHoi.Type.LongText:
                            {
                                var lstCauTraLoi = (dictKq[cauHoi.MaCauHoi]?.ToString() ?? "").Split("\n");
                                for (var i = 0; i < 5; i++)
                                {
                                    dictLongText.Add($"{cauHoi.MaCauHoi}_{i}", lstCauTraLoi.ElementAtOrDefault(i) ?? "……………………………………………………………………………………......");
                                }

                                break;
                            }
                        default:
                            dict.Add($"{cauHoi.MaCauHoi}", dictKq[cauHoi.MaCauHoi]?.ToString() ?? "……………………………………………………………………………………......");
                            break;
                    }
        }

        using var r = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), PathDocx));
        using (var ms = new MemoryStream())
        {
            await r.BaseStream.CopyToAsync(ms, cancellationToken);
            content = await Ultils.ReplaceDocxFile(ms.ToArray(), dict, dictSymbolChar, dictLongText);
        }

        return new DownloadPhieuDto
        {
            FileName = fileName,
            ContentType = "application/pdf",
            //ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            FileContents = await ConvertToPdf(content, cancellationToken)
        };
    }

    private static async Task<byte[]> ConvertToPdf(byte[] content, CancellationToken cancellationToken)
    {
        ComponentInfo.SetLicense("DH5L-ED6Q-R7O0-DY0H");
        var document = DocumentModel.Load(new MemoryStream(content));
        document.Save(Path.Combine(Directory.GetCurrentDirectory(), PathPdf));
        using var r2 = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), PathPdf));
        using var ms = new MemoryStream();
        await r2.BaseStream.CopyToAsync(ms, cancellationToken);
        return ms.ToArray();
    }
}