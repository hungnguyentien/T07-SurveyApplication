using AutoMapper;
using SurveyApplication.Application.DTOs.Account;
using SurveyApplication.Application.DTOs.BangKhaoSat;
using SurveyApplication.Application.DTOs.BaoCaoCauHoi;
using SurveyApplication.Application.DTOs.CauHoi;
using SurveyApplication.Application.DTOs.DonVi;
using SurveyApplication.Application.DTOs.DotKhaoSat;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.DTOs.LinhVucHoatDong;
using SurveyApplication.Application.DTOs.LoaiHinhDonVi;
using SurveyApplication.Application.DTOs.NguoiDaiDien;
using SurveyApplication.Application.DTOs.PhieuKhaoSat;
using SurveyApplication.Application.DTOs.QuanHuyen;
using SurveyApplication.Application.DTOs.Role;
using SurveyApplication.Application.DTOs.TinhTp;
using SurveyApplication.Application.DTOs.XaPhuong;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Models;

namespace SurveyApplication.Application.Profiles;

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

        CreateMap<CauHoi, CauHoiDto>().ReverseMap();
        CreateMap<Cot, CotDto>().ReverseMap();
        CreateMap<Hang, HangDto>().ReverseMap();
        CreateMap<KetQua, CreateKetQuaDto>().ReverseMap();
        CreateMap<CauHoi, CreateCauHoiDto>().ReverseMap();
        CreateMap<CauHoi, UpdateCauHoiDto>().ReverseMap();

        CreateMap<DonVi, DonViDto>().ReverseMap();
        CreateMap<DonVi, CreateDonViDto>().ReverseMap();
        CreateMap<DonVi, UpdateDonViDto>().ReverseMap();

        CreateMap<BangKhaoSatCauHoi, BangKhaoSatCauHoiDto>().ReverseMap();

        CreateMap<LinhVucHoatDong, LinhVucHoatDongDto>().ReverseMap();
        CreateMap<LinhVucHoatDong, CreateLinhVucHoatDongDto>().ReverseMap();
        CreateMap<LinhVucHoatDong, UpdateLinhVucHoatDongDto>().ReverseMap();


        CreateMap<XaPhuong, XaPhuongDto>().ReverseMap();
        CreateMap<XaPhuong, CreateXaPhuongDto>().ReverseMap();
        CreateMap<XaPhuong, UpdateXaPhuongDto>().ReverseMap();

        CreateMap<QuanHuyen, QuanHuyenDto>().ReverseMap();
        CreateMap<QuanHuyen, CreateQuanHuyenDto>().ReverseMap();
        CreateMap<QuanHuyen, UpdateQuanHuyenDto>().ReverseMap();

        CreateMap<TinhTp, TinhTpDto>().ReverseMap();
        CreateMap<TinhTp, CreateTinhTpDto>().ReverseMap();
        CreateMap<TinhTp, UpdateTinhTpDto>().ReverseMap();
        CreateMap<BaoCaoCauHoi, CreateBaoCaoCauHoiDto>().ReverseMap();

        CreateMap<ApplicationUser, AccountDto>().ReverseMap();
        CreateMap<CreateAccountDto, ApplicationUser>();

        CreateMap<Role, RoleDto>().ReverseMap();
    }
}