using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Commands;

public class ImportQuanHuyenCommandHandler : BaseMasterFeatures,
    IRequestHandler<ImportQuanHuyenCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;

    public ImportQuanHuyenCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
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
            var quanHuyenDto = item.Value;

            var entity = new QuanHuyen
            {
                Code = quanHuyenDto.Code,
                Name = quanHuyenDto.Name,
                Type = quanHuyenDto.Type,
                ParentCode = quanHuyenDto.parent_code
            };

            await _surveyRepo.QuanHuyen.Create(entity);
        }

        await _surveyRepo.SaveAync();

        return response;
    }
}