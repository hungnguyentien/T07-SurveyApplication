using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Commands;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features.LoaiHinhDonVi.Handlers.Commands;

public class DeleteLoaiHinhDonViCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteLoaiHinhDonViCommand>
{
    private readonly IMapper _mapper;

    public DeleteLoaiHinhDonViCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
        surveyRepository)
    {
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteLoaiHinhDonViCommand request, CancellationToken cancellationToken)
    {
        if (await _surveyRepo.DonVi.Exists(x => !x.Deleted && x.IdLoaiHinh == request.Id))
            throw new FluentValidation.ValidationException("Loại hình đơn vị đã được sử dụng");

        var loaiHinhDonViRepository = await _surveyRepo.LoaiHinhDonVi.GetById(request.Id);
        if (loaiHinhDonViRepository == null) throw new NotFoundException(nameof(LoaiHinhDonVi), request.Id);
        await _surveyRepo.LoaiHinhDonVi.Delete(loaiHinhDonViRepository);
        await _surveyRepo.SaveAync();
        return Unit.Value;
    }
}