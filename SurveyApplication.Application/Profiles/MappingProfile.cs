﻿using AutoMapper;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoaiHinhDonVi, LoaiHinhDonViDto>().ReverseMap();
            CreateMap<LoaiHinhDonVi, CreateLoaiHinhDonViDto>().ReverseMap();

            CreateMap<BangKhaoSat, BangKhaoSatDto>().ReverseMap();
            CreateMap<BangKhaoSat, CreateBangKhaoSatDto>().ReverseMap();

        }
    }
}
