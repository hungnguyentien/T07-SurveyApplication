using AutoMapper;
using MediatR;
using OfficeOpenXml;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands;

public class ImportDonViCommandhandler : BaseMasterFeatures, IRequestHandler<ImportDonViCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public ImportDonViCommandhandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(ImportDonViCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        try
        {
            using (var package = new ExcelPackage(request.File.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                for (var row = 2; row <= rowCount; row++)
                {
                    var idtinhTP = 0;
                    var idhuyen = 0;
                    var idxa = 0;
                    var codetinh = "";
                    var codehuyen = "";

                    var tenDonVi = worksheet.Cells[row, 2].Text;
                    var madonvi = worksheet.Cells[row, 4].Text;
                    var masothue = worksheet.Cells[row, 5].Text;
                    var diaChi = worksheet.Cells[row, 6].Text;
                    // Cắt chuỗi qua dấu ","
                    var parts = diaChi.Split(',');
                    var newParts = parts.Where(x => x.IndexOf("Việt Nam", StringComparison.OrdinalIgnoreCase) == -1)
                        .ToArray();
                    // Loại bỏ từ "Việt Nam" trong chuỗi
                    var tinh = newParts.Length >= 0 ? newParts[newParts.Length - 1].Trim() : diaChi;
                    var huyen = newParts.Length >= 0 ? newParts[newParts.Length - 2].Trim() : diaChi;
                    var xa = newParts.Length >= 0 ? newParts[newParts.Length - 3].Trim() : diaChi;
                    if (tinh != null)
                    {
                        var partsTinh = tinh.Split(" ");
                        var result = "";
                        for (var i = partsTinh.Length - 1; i >= 0; i--)
                        {
                            var part = partsTinh[i];
                            result = part + " " + result;
                            var findtinh =
                                await _surveyRepo.TinhTp.FirstOrDefaultAsync(x => x.Name == result && !x.Deleted);
                            if (findtinh != null)
                            {
                                idtinhTP = findtinh.Id;
                                codetinh = findtinh.Code;
                                break;
                            }
                        }
                    }

                    if (huyen != null)
                    {
                        var partsHuyen = huyen.Split(" ");
                        var result = "";
                        for (var i = partsHuyen.Length - 1; i >= 0; i--)
                        {
                            var part = partsHuyen[i];
                            result = part + " " + result;
                            var findhuyen =
                                await _surveyRepo.QuanHuyen.FirstOrDefaultAsync(x =>
                                    x.Name == result && x.ParentCode == codetinh);
                            if (findhuyen != null)
                            {
                                idhuyen = findhuyen.Id;
                                codehuyen = findhuyen.Code;
                                break;
                            }
                        }
                    }

                    if (xa != null)
                    {
                        var partsXa = xa.Split(" ");
                        var result = "";
                        for (var i = partsXa.Length - 1; i >= 0; i--)
                        {
                            var part = partsXa[i];
                            result = part + " " + result;
                            var findxa =
                                await _surveyRepo.XaPhuong.FirstOrDefaultAsync(x =>
                                    x.Name == result && x.ParentCode == codehuyen);
                            if (findxa != null)
                            {
                                idxa = findxa.Id;
                                break;
                            }
                        }
                    }

                    var cleanedDiaChi = string.Join(", ", newParts.Skip(0).Take(parts.Length - 4).ToArray());

                    var email = worksheet.Cells[row, 7].Text;
                    var sdt = worksheet.Cells[row, 8].Text;
                    var tenndd = worksheet.Cells[row, 9].Text;
                    var sdtndd = worksheet.Cells[row, 10].Text;
                    var entity = new DonVi
                    {
                        MaDonVi = madonvi,
                        TenDonVi = tenDonVi,
                        MaSoThue = masothue,
                        DiaChi = cleanedDiaChi,
                        Email = email,
                        SoDienThoai = sdt,
                        IdLinhVuc = null,
                        IdQuanHuyen = idhuyen,
                        IdXaPhuong = idxa,
                        IdLoaiHinh = null,
                        IdTinhTp = idtinhTP,
                        WebSite = null
                    };
                    var responceDonVi = await _surveyRepo.DonVi.Create(entity);
                    var ndd = new NguoiDaiDien
                    {
                        MaNguoiDaiDien = Guid.NewGuid().ToString(),
                        HoTen = tenndd,
                        SoDienThoai = sdtndd,
                        IdDonVi = responceDonVi.Id,
                        Email = null,
                        ChucVu = null,
                        MoTa = null
                    };
                    var responceNguoiDaiDien = await _surveyRepo.NguoiDaiDien.Create(ndd);
                }

                await _surveyRepo.SaveAync();
            }
        }
        catch (Exception ex)
        {
            // Ghi log thông điệp lỗi chi tiết
            //_logger.LogError(ex, "An error occurred while saving entity changes.");
            Console.WriteLine("{0}", ex);

            // Rethrow lỗi để nó hiển thị ở phía client
            throw;
        }

        return response;
    }
}