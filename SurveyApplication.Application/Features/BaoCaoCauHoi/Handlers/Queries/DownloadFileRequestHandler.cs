using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SurveyApplication.Application.DTOs.StgFile;
using SurveyApplication.Application.Features.BaoCaoCauHoi.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BaoCaoCauHoi.Handlers.Queries
{
    public class DownloadFileRequestHandler : BaseMasterFeatures, IRequestHandler<DownloadFileRequest, StgFileDto>
    {
        private readonly IMapper _mapper;

        public DownloadFileRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<StgFileDto> Handle(DownloadFileRequest request, CancellationToken cancellationToken)
        {
            var file = await _surveyRepo.StgFile.GetAsync(request.Id);

            // Kiểm tra xem file có tồn tại không
            if (file == null || !File.Exists(file.PhysicalPath))
            {
                return null; // Trả về HTTP 404 Not Found nếu file không tồn tại
            }
            if (string.IsNullOrEmpty(file.FileName))
            {
                return null; // Trả về HTTP 400 Bad Request nếu tên file không hợp lệ
            }

            byte[] fileContents = File.ReadAllBytes(file.PhysicalPath);

            return new StgFileDto
            {
                FileContents = fileContents,
                ContentType = file.ContentType,
                FileName = file.FileName,
                FileType = file.FileType,
            };

            //var stream = new FileStream(file.PhysicalPath, FileMode.Open);

            //return new FileStreamResult(stream, file.ContentType)
            //{
            //    FileDownloadName = file.FileName
            //};
        }
    }
}
