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
using Vanara.PInvoke;
using SurveyApplication.Application.DTOs.StgFile;

namespace SurveyApplication.Application.Features.PhieuKhaoSat.Handlers.Commands
{
    public class UploadFileCommandHandler : BaseMasterFeatures, IRequestHandler<UploadFileCommand, List<StgFileDto>>
    {
        private readonly IMapper _mapper;
        private StorageConfigs StorageConfigs { get; }

        public UploadFileCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper, IOptions<StorageConfigs> storageConfigs) : base(surveyRepository)
        {
            _mapper = mapper;
            StorageConfigs = storageConfigs.Value;
        }

        public async Task<List<StgFileDto>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var uploadFileDto = request.UploadFileDto;

            if (uploadFileDto == null || uploadFileDto.files == null || uploadFileDto.files.Count == 0)
            {
                return null;
            }

            var lstStgFiles = new List<StgFileDto>();

            foreach (var file in uploadFileDto.files)
            {
                if (file.Length > 0)
                {
                    string yearMonthDayPath = Path.Combine(StorageConfigs.BaseStorage, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());

                    // Tạo thư mục nếu nó chưa tồn tại
                    Directory.CreateDirectory(yearMonthDayPath);

                    var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);

                    var filePath = Path.Combine(yearMonthDayPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    byte[] fileContents = File.ReadAllBytes(filePath);

                    var stgFile = new StgFileDto
                    {
                        FileContents= fileContents,
                        FileName = file.FileName,
                        PhysicalPath = filePath,
                        ContentType = file.ContentType,
                        Size = file.Length,
                    };

                    var stg = _mapper.Map<StgFile>(new StgFileDto
                    {
                        FileContents = fileContents,
                        FileName = file.FileName,
                        PhysicalPath = filePath,
                        ContentType = file.ContentType,
                        Size = file.Length,
                    });

                    stg = await _surveyRepo.StgFile.Create(stg);
                    await _surveyRepo.SaveAync();

                    stgFile.Id= stg.Id;
                    lstStgFiles.Add(stgFile);
                }
            }
            return lstStgFiles;
        }
    }
}
