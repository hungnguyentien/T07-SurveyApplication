﻿using FluentValidation;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.DTOs.DotKhaoSat.Validators;

public class IDotKhaoSatDtoValidator : AbstractValidator<IDotKhaoSatDto>
{
    private readonly IDotKhaoSatRepository _dotKhaoSatRepository;

    public IDotKhaoSatDtoValidator(IDotKhaoSatRepository dotKhaoSatRepository)
    {
        _dotKhaoSatRepository = dotKhaoSatRepository;

        RuleFor(p => p.IdLoaiHinh).GreaterThan(0).WithMessage("{PropertyName} phải lớn hơn 0.");

        RuleFor(p => p.TenDotKhaoSat)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.NgayBatDau)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .Must((rootObject, ngayBatDau) => BeAValidDate(ngayBatDau, rootObject.NgayKetThuc))
            .WithMessage("{PropertyName} must be a valid date and earlier than NgayKetThuc.");

        RuleFor(p => p.NgayKetThuc)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .Must((rootObject, ngayKetThuc) => BeAValidDate(rootObject.NgayBatDau, ngayKetThuc))
            .WithMessage("{PropertyName} must be a valid date and later than NgayBatDau.");
    }

    private bool BeAValidDate(DateTime? ngayBatDau, DateTime? ngayKetThuc)
    {
        if (ngayBatDau.HasValue && ngayKetThuc.HasValue) return ngayBatDau <= ngayKetThuc;
        return true;
    }
}