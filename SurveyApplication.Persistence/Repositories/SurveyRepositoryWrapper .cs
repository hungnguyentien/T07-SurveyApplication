using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories;

public class SurveyRepositoryWrapper : ISurveyRepositoryWrapper
{
    //RenderHere
    public async Task SaveAync()
    {
        await _repoContext.SaveChangesAsync();
    }

    #region Ctor

    private readonly SurveyApplicationDbContext _repoContext;

    public SurveyRepositoryWrapper(SurveyApplicationDbContext repositoryContext)
    {
        _repoContext = repositoryContext;
    }

    #endregion

    #region Properties

    private ICauHoiRepository _cauHoiRepository;
    public ICauHoiRepository CauHoi => _cauHoiRepository ??= new CauHoiRepository(_repoContext);

    private ICotRepository _cotRepository;
    public ICotRepository Cot => _cotRepository ??= new CotRepository(_repoContext);

    private IHangRepository _hangRepository;
    public IHangRepository Hang => _hangRepository ??= new HangRepository(_repoContext);

    private IBangKhaoSatRepository _bangKhaoSatRepository;
    public IBangKhaoSatRepository BangKhaoSat => _bangKhaoSatRepository ??= new BangKhaoSatRepository(_repoContext);

    private IBangKhaoSatCauHoiRepository _bangKhaoSatCauHoiRepository;

    public IBangKhaoSatCauHoiRepository BangKhaoSatCauHoi =>
        _bangKhaoSatCauHoiRepository ??= new BangKhaoSatCauHoiRepository(_repoContext);

    private IKetQuaRepository _ketQuaRepository;
    public IKetQuaRepository KetQua => _ketQuaRepository ??= new KetQuaRepository(_repoContext);

    private IDonViRepository _donViRepository;
    public IDonViRepository DonVi => _donViRepository ??= new DonViRepository(_repoContext);

    private INguoiDaiDienRepository _nDaiDienRepository;
    public INguoiDaiDienRepository NguoiDaiDien => _nDaiDienRepository ??= new NguoiDaiDienRepository(_repoContext);

    public ILoaiHinhDonViRepository _loaiHinhDonViRepository;

    public ILoaiHinhDonViRepository LoaiHinhDonVi =>
        _loaiHinhDonViRepository ??= new LoaiHinhDonViRepository(_repoContext);

    public ILinhVucHoatDongRepository _linhVucHoatDongRepository;

    public ILinhVucHoatDongRepository LinhVucHoatDong =>
        _linhVucHoatDongRepository ??= new LinhVucHoatDongRepository(_repoContext);

    public IDotKhaoSatRepository _dotKhaoSatRepository;
    public IDotKhaoSatRepository DotKhaoSat => _dotKhaoSatRepository ??= new DotKhaoSatRepository(_repoContext);

    public IGuiEmailRepository _guiEmailRepository;
    public IGuiEmailRepository GuiEmail => _guiEmailRepository ??= new GuiEmailRepository(_repoContext);

    public IAccountRepository _accountRepository;
    public IAccountRepository Account => _accountRepository ??= new AccountRepository(_repoContext);

    public IXaPhuongRepository _xaPhuongRepository;
    public IXaPhuongRepository XaPhuong => _xaPhuongRepository ??= new XaPhuongRepository(_repoContext);

    public IQuanHuyenRepository _quanHuyenRepository;
    public IQuanHuyenRepository QuanHuyen => _quanHuyenRepository ??= new QuanHuyenRepository(_repoContext);

    public ITinhTpRepository _tinhTpRepository;
    public ITinhTpRepository TinhTp => _tinhTpRepository ??= new TinhTpRepository(_repoContext);

    public IBaoCaoCauHoiRepository _baoCaoCauHoiRepository;
    public IBaoCaoCauHoiRepository BaoCaoCauHoi => _baoCaoCauHoiRepository ??= new BaoCaoCauHoiRepository(_repoContext);

    public IJobScheduleRepository _jobScheduleRepository;
    public IJobScheduleRepository JobSchedule => _jobScheduleRepository ??= new JobScheduleRepository(_repoContext);

    public IRoleRepository _roleRepository;
    public IRoleRepository Role => _roleRepository ??= new RoleRepository(_repoContext);

    public IModuleRepository _moduleRepository;
    public IModuleRepository Module => _moduleRepository ??= new ModuleRepository(_repoContext);

    public IReleaseHistoryRepository _releaseHistoryRepository;

    public IReleaseHistoryRepository ReleaseHistory =>
        _releaseHistoryRepository ??= new ReleaseHistoryRepository(_repoContext);

    public IStgFileRepository _stgFileRepository;
    public IStgFileRepository StgFile => _stgFileRepository ??= new StgFileRepository(_repoContext);

    #endregion
}