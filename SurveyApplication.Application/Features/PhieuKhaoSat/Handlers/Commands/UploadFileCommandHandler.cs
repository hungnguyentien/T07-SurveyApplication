using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.PhieuKhaoSat.Validators;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;
using SurveyApplication.Utility.Enums;
using SurveyApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using SurveyApplication.Application.Exceptions;
using System.IO;
using SurveyApplication.Domain.Models;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class UploadFileCommandHandler : BaseMasterFeatures, IRequestHandler<UploadFileCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private StorageConfigs StorageConfigs { get; }

        public UploadFileCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, IOptions<StorageConfigs> storageConfigs) : base(surveyRepository)
        {
            _mapper = mapper;
            StorageConfigs = storageConfigs.Value;
        }

        public async Task<BaseCommandResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var uploadFileDto = request.UploadFileDto;

            if (uploadFileDto == null || uploadFileDto.files == null || uploadFileDto.files.Count == 0)
            {
                return new BaseCommandResponse("Không có tệp nào được tải lên.");
            }

            foreach (var file in uploadFileDto.files)
            {
                if (file.Length > 0)
                {
                    string yearMonthDayPath = Path.Combine(StorageConfigs.BaseStorage, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());

                    // Tạo thư mục nếu nó chưa tồn tại
                    Directory.CreateDirectory(yearMonthDayPath);

                    // Kiểm tra nếu thư mục không tồn tại thì tạo mới
                    if (!Directory.Exists(yearMonthDayPath))
                    {
                        Directory.CreateDirectory(yearMonthDayPath);
                    }

                    var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);

                    var filePath = Path.Combine(yearMonthDayPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var stgFile = new StgFile
                    {
                        FileName = fileName,
                        PhysicalPath = filePath,
                        ContentType = file.ContentType,
                        Size = file.Length,
                    };

                    _surveyRepo.StgFile.Create(stgFile);
                    _surveyRepo.SaveAync();
                }
            }

            return new BaseCommandResponse("Tải lên tệp thành công.");
        }
    }
}
