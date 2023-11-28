using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.DTOs.QuanHuyen.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Commands
{
    public class ImportQuanHuyenCommandHandler : BaseMasterFeatures, IRequestHandler<ImportQuanHuyenCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public ImportQuanHuyenCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(ImportQuanHuyenCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            using var reader = new StreamReader(request.File.OpenReadStream());
            var jsonData = await reader.ReadToEndAsync();

            var jsonDataDto = JsonConvert.DeserializeObject<Dictionary<string, QuanHuyenDto>>(jsonData);

            foreach (var item in jsonDataDto)
            {
                QuanHuyenDto quanHuyenDto = item.Value;

                QuanHuyen entity = new QuanHuyen
                {
                    Code = quanHuyenDto.Code,
                    Name = quanHuyenDto.Name,
                    Type = quanHuyenDto.Type,
                    ParentCode = quanHuyenDto.parent_code,
                };

                await _surveyRepo.QuanHuyen.Create(entity);
            }
            await _surveyRepo.SaveAync();

            return response;
        }
    }
}
