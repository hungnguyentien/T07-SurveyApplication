using AutoMapper;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
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
            CreateMap<LoaiHinhDonVi, UpdateLoaiHinhDonViDto>().ReverseMap();

            CreateMap<BangKhaoSat, BangKhaoSatDto>().ReverseMap();
            CreateMap<BangKhaoSat, CreateBangKhaoSatDto>().ReverseMap();
            CreateMap<BangKhaoSat, UpdateBangKhaoSatDto>().ReverseMap();

            CreateMap<DotKhaoSat, DotKhaoSatDto>().ReverseMap();
            CreateMap<DotKhaoSat, CreateDotKhaoSatDto>().ReverseMap();
            CreateMap<DotKhaoSat, UpdateDotKhaoSatDto>().ReverseMap();


            CreateMap<DonVi, DonViDto>().ReverseMap();
            CreateMap<DonVi, CreateDonViDto>().ReverseMap();
            CreateMap<DonVi, UpdateDonViDto>().ReverseMap();

            CreateMap<NguoiDaiDien, NguoiDaiDienDto>().ReverseMap();
            CreateMap<NguoiDaiDien, CreateNguoiDaiDienDto>().ReverseMap();
            CreateMap<NguoiDaiDien, UpdateNguoiDaiDienDto>().ReverseMap();

            CreateMap<GuiEmail, GuiEmailDto>().ReverseMap();
            CreateMap<GuiEmail, CreateGuiEmailDto>().ReverseMap();
            CreateMap<GuiEmail, UpdateGuiEmailDto>().ReverseMap();
        }
    }
}
