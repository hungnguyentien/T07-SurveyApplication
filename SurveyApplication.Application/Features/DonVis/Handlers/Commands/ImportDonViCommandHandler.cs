using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.DTOs.DonVi;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace SurveyApplication.Application.Features.DonVis.Handlers.Commands
{
    public class ImportDonViCommandHandler : BaseMasterFeatures, IRequestHandler<ImportDonViCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public ImportDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(ImportDonViCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            if (request.File == null || request.File.Length == 0)
            {
                return new BaseCommandResponse("Tệp Excel không hợp lệ!");
            }

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var stream = request.File.OpenReadStream())
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];

                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    var data = new DonVi();

                    data.TenDonVi = worksheet.Cells[row, 2].Value.ToString();
                    data.SoDienThoai = worksheet.Cells[row, 3].Value.ToString();

                    await _surveyRepo.DonVi.Create(data);
                }
                await _surveyRepo.SaveAync();
            }

            return response;
        }
    }
}
