using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SurveyApplication.Application.Features.PhieuKhaoSat.Requests.Commands;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain;
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
            var lstStgFiles = new List<StgFileDto>();
            if (uploadFileDto?.Files == null || uploadFileDto.Files.Count == 0)
                return lstStgFiles;

            foreach (var file in uploadFileDto.Files)
            {
                if (file.Length <= 0) continue;
                var yearMonthDayPath = Path.Combine(StorageConfigs.BaseStorage, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());
                // Tạo thư mục nếu nó chưa tồn tại
                Directory.CreateDirectory(yearMonthDayPath);
                var fileName = DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(yearMonthDayPath, fileName);
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream, cancellationToken);
                }

                var fileContents = await File.ReadAllBytesAsync(filePath, cancellationToken);
                var physicalPath = filePath[StorageConfigs.BaseStorage.Length..];
                var stgFile = new StgFileDto
                {
                    FileContents = fileContents,
                    FileName = file.FileName,
                    PhysicalPath = physicalPath,
                    ContentType = file.ContentType,
                    Size = file.Length,
                };

                var stg = _mapper.Map<StgFile>(new StgFileDto
                {
                    FileContents = fileContents,
                    FileName = file.FileName,
                    PhysicalPath = physicalPath,
                    ContentType = file.ContentType,
                    Size = file.Length,
                });
                stg = await _surveyRepo.StgFile.Create(stg);
                await _surveyRepo.SaveAync();
                stgFile.Id = stg.Id;
                lstStgFiles.Add(stgFile);
            }
            return lstStgFiles;
        }
    }
}
