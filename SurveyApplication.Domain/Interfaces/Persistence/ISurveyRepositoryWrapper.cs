namespace SurveyApplication.Domain.Interfaces.Persistence
{
    public interface ISurveyRepositoryWrapper
    {
        ICauHoiRepository CauHoi { get; }
        ICotRepository Cot { get; }
        IHangRepository Hang { get; }
        IBangKhaoSatRepository BangKhaoSat { get; }
        IBangKhaoSatCauHoiRepository BangKhaoSatCauHoi { get; }
        IKetQuaRepository KetQua { get; }
        IDonViRepository DonVi { get; }
        INguoiDaiDienRepository NguoiDaiDien { get; }

        //RenderHere
        Task SaveAync();
    }
}
