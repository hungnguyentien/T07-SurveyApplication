﻿using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Commands;
using SurveyApplication.Application.Features.DotKhaoSats.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.DotKhaoSats.Handlers.Commands
{
   
    public class UpdateGuiEmailCommandHandler : IRequestHandler<UpdateGuiEmailCommand, Unit>
    {
        private readonly IDotKhaoSatRepository _dotKhaoSatRepository;
        private readonly IMapper _mapper;

        public UpdateGuiEmailCommandHandler(IDotKhaoSatRepository dotKhaoSatRepository, IMapper mapper)
        {
            _dotKhaoSatRepository = dotKhaoSatRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGuiEmailCommand request, CancellationToken cancellationToken)
        {
            var dotKhaoSat = await _dotKhaoSatRepository.GetById(request.DotKhaoSatDto?.Id ?? 0);
            _mapper.Map(request.DotKhaoSatDto, dotKhaoSat);
            await _dotKhaoSatRepository.Update(dotKhaoSat);
            return Unit.Value;
        }
    }
}
