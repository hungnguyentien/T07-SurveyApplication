using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.DTOs.TinhTp.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Commands
{
    public class ImportTinhTpCommandHandler : BaseMasterFeatures, IRequestHandler<ImportTinhTpCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public ImportTinhTpCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(ImportTinhTpCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            using var reader = new StreamReader(request.File.OpenReadStream());
            var jsonData = await reader.ReadToEndAsync();

            var jsonDataDto = JsonConvert.DeserializeObject<Dictionary<string, TinhTpDto>>(jsonData);

            foreach (var item in jsonDataDto)
            {
                TinhTpDto TinhTpDto = item.Value;

                TinhTp entity = new TinhTp
                {
                    Code = TinhTpDto.Code,
                    Name = TinhTpDto.Name,
                    Type = TinhTpDto.Type,
                };

                await _surveyRepo.TinhTp.Create(entity);
            }
            await _surveyRepo.SaveAync();

            return response;
        }
    }
}
