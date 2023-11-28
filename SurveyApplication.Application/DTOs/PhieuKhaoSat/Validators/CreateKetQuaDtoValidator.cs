using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.PhieuKhaoSat.Validators;

public class CreateKetQuaDtoValidator : AbstractValidator<CreateKetQuaDto>
{
    private readonly IKetQuaRepository _ketQuaRepository;

    public CreateKetQuaDtoValidator(IKetQuaRepository ketQuaRepository)
    {
        _ketQuaRepository = ketQuaRepository;
        Include(new KetQuaDtoValidator(_ketQuaRepository));
    }
}