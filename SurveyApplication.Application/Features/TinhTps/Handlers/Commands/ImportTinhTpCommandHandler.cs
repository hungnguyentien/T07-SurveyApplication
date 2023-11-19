using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.Features.TinhTps.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.TinhTps.Handlers.Commands;

public class ImportTinhTpCommandHandler : BaseMasterFeatures, IRequestHandler<ImportTinhTpCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public ImportTinhTpCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
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
            var TinhTpDto = item.Value;

            var entity = new TinhTp
            {
                Code = TinhTpDto.Code,
                Name = TinhTpDto.Name,
                Type = TinhTpDto.Type
            };

            await _surveyRepo.TinhTp.Create(entity);
        }

        await _surveyRepo.SaveAync();

        return response;
    }
}