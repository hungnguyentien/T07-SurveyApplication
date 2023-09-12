using AutoMapper;
using MediatR;
using SurveyApplication.Application.DTOs.QuanHuyen.Validators;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.QuanHuyens.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.QuanHuyens.Handlers.Commands
{
    //public class ImportQuanHuyenCommandHandler : BaseMasterFeatures, IRequestHandler<ImportQuanHuyenCommand, BaseCommandResponse>
    //{
    //    private readonly IMapper _mapper;

    //    public ImportQuanHuyenCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
    //    {
    //        _mapper = mapper;
    //    }

    //    public async Task<BaseCommandResponse> Handle(ImportQuanHuyenCommand request, CancellationToken cancellationToken)
    //    {
    //        var response = new BaseCommandResponse();
    //        var validator = new CreateQuanHuyenDtoValidator(_surveyRepo.QuanHuyen);
    //        var validatorResult = await validator.ValidateAsync(request.QuanHuyenDto);

    //        if (validatorResult.IsValid == false)
    //        {
    //            response.Success = false;
    //            response.Message = "Tạo mới thất bại";
    //            response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();
    //            throw new ValidationException(validatorResult);
    //        }

    //        var QuanHuyen = _mapper.Map<QuanHuyen>(request.QuanHuyenDto);

    //        QuanHuyen = await _surveyRepo.QuanHuyen.Create(QuanHuyen);
    //        await _surveyRepo.SaveAync();

    //        response.Success = true;
    //        response.Message = "Tạo mới thành công";
    //        response.Id = QuanHuyen.Id;
    //        return response;
    //    }
    //}
}
