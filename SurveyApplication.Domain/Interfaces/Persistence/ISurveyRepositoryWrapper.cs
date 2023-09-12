namespace SurveyApplication.Domain.Interfaces.Persistence
{
    public interface ISurveyRepositoryWrapper
    {
        ICauHoiRepository CauHoi { get; }
        ICotRepository Cot { get; }
        IHangRepository Hang { get; }
        IBangKhaoSatRepository BangKhaoSat { get; }
        ILoaiHinhDonViRepository LoaiHinhDonVi { get; }
        IDotKhaoSatRepository DotKhaoSat { get; }
        IGuiEmailRepository GuiEmail { get; }
        ILinhVucHoatDongRepository LinhVucHoatDong { get; }
        IBangKhaoSatCauHoiRepository BangKhaoSatCauHoi { get; }
        IKetQuaRepository KetQua { get; }
        IDonViRepository DonVi { get; }
        INguoiDaiDienRepository NguoiDaiDien { get; }
        IAccountRepository Account { get; }
        IXaPhuongRepository XaPhuong { get; }
        IQuanHuyenRepository QuanHuyen { get; }
        ITinhTpRepository TinhTp { get; }


        //RenderHere
        Task SaveAync();
    }
}
