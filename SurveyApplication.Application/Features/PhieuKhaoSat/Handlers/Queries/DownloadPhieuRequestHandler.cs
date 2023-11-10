using MediatR;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Queries;
using System.Collections;
using System.Globalization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain.Interfaces.Persistence;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Utility;
using SurveyApplication.Utility.Enums;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Queries
{
    public class DownloadPhieuRequestHandler : BaseMasterFeatures,
        IRequestHandler<DownloadPhieuRequest, DownloadPhieuDto>
    {
        private EmailSettings EmailSettings { get; }
        private const string PathJson = @"TempData\MAU_PKS_DOANH_NGHIEP.docx";

        public DownloadPhieuRequestHandler(ISurveyRepositoryWrapper surveyRepository, IOptions<EmailSettings> emailSettings) : base(
            surveyRepository)
        {
            EmailSettings = emailSettings.Value;
        }

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
            var donVi = await _surveyRepo.DonVi.FirstOrDefaultAsync(x => !x.Deleted && x.Id == guiEmail.IdDonVi) ?? new DonVi();
            var nguoiDaiDien = await _surveyRepo.NguoiDaiDien.FirstOrDefaultAsync(x => !x.Deleted && x.Id == guiEmail.IdDonVi) ?? new NguoiDaiDien();

            byte[] content;
            var fileName = $"PKS_{bks?.TenBangKhaoSat}_{donVi.TenDonVi}_MST_{donVi.MaSoThue}.docx";
            var dict = new Dictionary<string, string>
            {
                { "HO_TEN", nguoiDaiDien.HoTen },
                { "CHUC_VU", nguoiDaiDien.ChucVu ?? "" },
                { "DIEN_THOAI", nguoiDaiDien.SoDienThoai },
                { "EMAIL", nguoiDaiDien.Email ?? "" },
                { "TEN_DON_VI", donVi.TenDonVi },
                { "DIA_CHI", donVi.DiaChi ?? "" },
                { "DIEN_THOAI_2", donVi.SoDienThoai },
                { "EMAIL_2", donVi.Email },
                { "Day", DateTime.Now.Day.ToString(CultureInfo.InvariantCulture) },
                { "Month", DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) }
            };
            var dictSymbolChar = new Dictionary<string, string>();
            var ketQua = await _surveyRepo.KetQua.FirstOrDefaultAsync(x => !x.Deleted && x.IdGuiEmail == guiEmail.Id);
            if (ketQua != null)
            {
                var lstIdCauHoi = await _surveyRepo.BangKhaoSatCauHoi.GetAllQueryable().AsNoTracking()
                    .Where(x => !x.Deleted && x.IdBangKhaoSat == guiEmail.IdBangKhaoSat).Select(x => x.IdCauHoi).ToListAsync(cancellationToken: cancellationToken);
                var dictKq = JsonConvert.DeserializeObject<Hashtable>(ketQua.Data);
                var lstCauHoi = await _surveyRepo.CauHoi.GetAllListAsync(x => !x.Deleted && lstIdCauHoi.Contains(x.Id));
                var lstHang = await _surveyRepo.Hang.GetAllQueryable().AsNoTracking()
                    .Where(x => !x.Deleted && lstIdCauHoi.Contains(x.IdCauHoi)).ToListAsync(cancellationToken: cancellationToken);
                var lstCot = await _surveyRepo.Cot.GetAllQueryable().AsNoTracking()
                    .Where(x => !x.Deleted && lstIdCauHoi.Contains(x.IdCauHoi)).ToListAsync(cancellationToken: cancellationToken);
                foreach (var cauHoi in lstCauHoi.Where(x => dictKq != null && dictKq.ContainsKey(x.MaCauHoi)))
                {
                    if (dictKq != null)
                    {
                        switch (cauHoi.LoaiCauHoi)
                        {
                            case (int)EnumCauHoi.Type.Radio:
                            {
                                dictSymbolChar.Add($"{cauHoi.MaCauHoi}_{dictKq[cauHoi.MaCauHoi]?.ToString().ConvertToCamelString()}", "F09C");
                                if (!dict.ContainsKey($"{cauHoi.MaCauHoi}_Comment"))
                                    dict.Add($"{cauHoi.MaCauHoi}_Comment", dictKq[$"{cauHoi.MaCauHoi}-Comment"]?.ToString() ?? "");
                                break;
                            }
                            case (int)EnumCauHoi.Type.CheckBox:
                            {
                                var lstCauTraLoi = JsonConvert.DeserializeObject<List<string>>(dictKq[cauHoi.MaCauHoi]?.ToString() ?? "") ?? new List<string>();
                                foreach (var cauTraLoi in lstCauTraLoi)
                                {
                                    dictSymbolChar.Add($"{cauHoi.MaCauHoi}_{cauTraLoi.ConvertToCamelString()}", "F052");
                                    if (!dict.ContainsKey($"{cauHoi.MaCauHoi}_Comment"))
                                        dict.Add($"{cauHoi.MaCauHoi}_Comment", dictKq[$"{cauHoi.MaCauHoi}-Comment"]?.ToString() ?? "");
                                }

                                break;
                            }
                            case (int)EnumCauHoi.Type.MultiTextMatrix:
                            {
                                var objCauTraLoi = JsonConvert.DeserializeObject<Hashtable>(dictKq[cauHoi.MaCauHoi]?.ToString() ?? "");
                                foreach (var hang in lstHang.Where(x => x.IdCauHoi == cauHoi.Id))
                                {
                                    var objCauTraLoiHang =
                                        JsonConvert.DeserializeObject<Hashtable>(objCauTraLoi?[hang.MaHang]?.ToString() ?? "");
                                    foreach (var cot in lstCot.Where(x => x.IdCauHoi == cauHoi.Id))
                                    {
                                        dict.Add($"{cauHoi.MaCauHoi}_{hang.MaHang}_{cot.MaCot}", objCauTraLoiHang?[cot.MaCot]?.ToString() ?? "");
                                    }
                                }

                                break;
                            }
                            default:
                                dict.Add($"{cauHoi.MaCauHoi}", dictKq[cauHoi.MaCauHoi]?.ToString() ?? "");
                                break;
                        }
                    }
                }
            }

            using var r = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), PathJson));
            using (var ms = new MemoryStream())
            {
                await r.BaseStream.CopyToAsync(ms, cancellationToken);
                content = await Ultils.ReplaceDocxFile(ms.ToArray(), dict, dictSymbolChar);
            }

            return new DownloadPhieuDto
            {
                FileName = fileName,
                ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                FileContents = content
            };
        }
    }
}
