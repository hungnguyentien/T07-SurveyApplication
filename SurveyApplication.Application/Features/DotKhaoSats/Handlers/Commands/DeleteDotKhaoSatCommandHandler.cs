using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands;

public class DeleteDotKhaoSatCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteDotKhaoSatCommand>
{
    private readonly IMapper _mapper;

    public DeleteDotKhaoSatCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteDotKhaoSatCommand request, CancellationToken cancellationToken)
    {
        if (await _surveyRepo.BangKhaoSat.Exists(x => x.IdDotKhaoSat == request.Id)) 
            throw new FluentValidation.ValidationException("Đợt khảo sát đã được sử dụng");
        var dotKhaoSatRepository = await _surveyRepo.DotKhaoSat.GetById(request.Id);
        if (dotKhaoSatRepository == null) throw new NotFoundException(nameof(DotKhaoSat), request.Id);
        await _surveyRepo.DotKhaoSat.Delete(dotKhaoSatRepository);
        await _surveyRepo.SaveAync();

        return Unit.Value;
    }
}