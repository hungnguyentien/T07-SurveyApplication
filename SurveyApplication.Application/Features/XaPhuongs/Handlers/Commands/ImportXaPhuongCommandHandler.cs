using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Application.Features.XaPhuongs.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.XaPhuongs.Handlers.Commands;

public class ImportXaPhuongCommandHandler : BaseMasterFeatures,
    IRequestHandler<ImportXaPhuongCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public ImportXaPhuongCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(ImportXaPhuongCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        using var reader = new StreamReader(request.File.OpenReadStream());
        var jsonData = await reader.ReadToEndAsync();

        var jsonDataDto = JsonConvert.DeserializeObject<Dictionary<string, XaPhuongDto>>(jsonData);

        foreach (var item in jsonDataDto)
        {
            var XaPhuongDto = item.Value;

            var entity = new XaPhuong
            {
                Code = XaPhuongDto.Code,
                Name = XaPhuongDto.Name,
                Type = XaPhuongDto.Type,
                ParentCode = XaPhuongDto.parent_code
            };

            await _surveyRepo.XaPhuong.Create(entity);
        }

        await _surveyRepo.SaveAync();

        return response;
    }
}