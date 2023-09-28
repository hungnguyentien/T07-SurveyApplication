﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Application.Features.BangKhaoSats.Requests.Queries;
using SurveyApplication.Application.Features.LoaiHinhDonVi.Requests.Queries;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.BangKhaoSats.Handlers.Queries
{
    public class GetLastRecordBangKhaoSatRequestHandler : BaseMasterFeatures, IRequestHandler<GetLastRecordBangKhaoSatRequest, string>
    {
        private readonly IMapper _mapper;

        public GetLastRecordBangKhaoSatRequestHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(
            surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<string> Handle(GetLastRecordBangKhaoSatRequest request, CancellationToken cancellationToken)
        {
            var lastEntity = await _surveyRepo.BangKhaoSat.GetAllQueryable().OrderByDescending(e => e.Id).FirstOrDefaultAsync();

            if (lastEntity != null)
            {
                var prefix = lastEntity.MaBangKhaoSat.Substring(0, 3);
                var currentNumber = int.Parse(lastEntity.MaBangKhaoSat.Substring(3));

                currentNumber++;
                var newNumber = currentNumber.ToString("D3");

                return prefix + newNumber;
            }

            return "BKS001";
        }
    }
}
