using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands
{
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
                using (var package = new ExcelPackage(request.file.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        int idtinhTP = 0;
                        int idhuyen = 0;
                        int idxa = 0;
                        string codetinh = "";
                        string codehuyen = "";
                       
                        string tenDonVi = worksheet.Cells[row, 2].Text;
                        string madonvi = worksheet.Cells[row, 4].Text;
                        string masothue = worksheet.Cells[row, 5].Text;
                        string diaChi = worksheet.Cells[row, 6].Text;
                        // Cắt chuỗi qua dấu ","
                        string[] parts = diaChi.Split(',');
                        string[] newParts = parts.Where(x => x.IndexOf("Việt Nam") == -1).ToArray();
                        // Loại bỏ từ "Việt Nam" trong chuỗi
                        string tinh = newParts.Length >= 0 ? newParts[newParts.Length - 1].Trim() : diaChi;
                        string huyen = newParts.Length >= 0 ? newParts[newParts.Length - 2].Trim() : diaChi;
                        string xa = newParts.Length >= 0 ? newParts[newParts.Length - 3].Trim() : diaChi;
                        if(tinh != null)
                        {
                            string[] partsTinh = tinh.Split(" ");
                            string result = "";
                            for (int i = partsTinh.Length - 1; i >= 0; i--)
                            {
                                string part = partsTinh[i];
                                result = part + " " + result;
                                var findtinh = await _surveyRepo.TinhTp.FirstOrDefaultAsync(x => x.Name == result && !x.Deleted);
                                if (findtinh != null)
                                {
                                    idtinhTP = findtinh.Id;
                                    codetinh = findtinh.Code;
                                    break;
                                }
                            }
                        }
                        if(huyen != null)
                        {
                            string[] partsHuyen = huyen.Split(" ");
                            string result = "";
                            for (int i = partsHuyen.Length - 1; i >= 0; i--)
                            {
                                string part = partsHuyen[i];
                                result = part + " " + result;
                                var findhuyen = await _surveyRepo.QuanHuyen.FirstOrDefaultAsync(x => x.Name == result && x.ParentCode == codetinh);
                                if (findhuyen != null)
                                {
                                    idhuyen = findhuyen.Id;
                                    codehuyen= findhuyen.Code;
                                    break;
                                }
                            }
                        }
                        if (xa != null)
                        {
                            string[] partsXa = xa.Split(" ");
                            string result = "";
                            for (int i = partsXa.Length - 1; i >= 0; i--)
                            {
                                string part = partsXa[i];
                                result = part + " " + result;
                                var findxa = await _surveyRepo.XaPhuong.FirstOrDefaultAsync(x => x.Name == result && x.ParentCode == codehuyen);
                                if (findxa != null)
                                {
                                    idxa = findxa.Id;
                                    break;
                                }
                            }
                        }
                        
                        string cleanedDiaChi = string.Join(", ", newParts.Skip(0).Take(parts.Length - 4).ToArray());
                        
                        string email = worksheet.Cells[row, 7].Text;
                        string sdt = worksheet.Cells[row, 8].Text;
                        string tenndd = worksheet.Cells[row, 9].Text;
                        string sdtndd = worksheet.Cells[row, 10].Text;
                        DonVi entity = new DonVi
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
                            WebSite = null,
                        };
                        var responceDonVi = await _surveyRepo.DonVi.Create(entity);
                        NguoiDaiDien ndd = new NguoiDaiDien
                        {
                            MaNguoiDaiDien = Guid.NewGuid().ToString(),
                            HoTen = tenndd,
                            SoDienThoai = sdtndd,
                            IdDonVi = responceDonVi.Id,
                            Email = null,
                            ChucVu = null,
                            MoTa = null,

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
}

