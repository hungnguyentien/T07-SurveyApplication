using Microsoft.Extensions.Configuration;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence.Repositories
{
    public class SurveyRepositoryWrapper : ISurveyRepositoryWrapper
    {
        #region Ctor

        private readonly SurveyApplicationDbContext _repoContext;
        private readonly IConfiguration _configuration;
        public SurveyRepositoryWrapper(SurveyApplicationDbContext repositoryContext, IConfiguration configuration)
        {
            _repoContext = repositoryContext;
            _configuration = configuration;
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
        public IBangKhaoSatCauHoiRepository BangKhaoSatCauHoi => _bangKhaoSatCauHoiRepository ??= new BangKhaoSatCauHoiRepository(_repoContext);

        private IKetQuaRepository _ketQuaRepository;
        public IKetQuaRepository KetQua => _ketQuaRepository ??= new KetQuaRepository(_repoContext);

        private IDonViRepository _donViRepository;
        public IDonViRepository DonVi => _donViRepository ??= new DonViRepository(_repoContext);

        private INguoiDaiDienRepository _nDaiDienRepository;
        public INguoiDaiDienRepository NguoiDaiDien => _nDaiDienRepository ??= new NguoiDaiDienRepository(_repoContext);
        
        public ILoaiHinhDonViRepository _loaiHinhDonViRepository;
        public ILoaiHinhDonViRepository LoaiHinhDonVi => _loaiHinhDonViRepository ??= new LoaiHinhDonViRepository(_repoContext);
        
        public ILinhVucHoatDongRepository _linhVucHoatDongRepository;
        public ILinhVucHoatDongRepository LinhVucHoatDong => _linhVucHoatDongRepository ??= new LinhVucHoatDongRepository(_repoContext);

        public IDotKhaoSatRepository _dotKhaoSatRepository;
        public IDotKhaoSatRepository DotKhaoSat => _dotKhaoSatRepository ??= new DotKhaoSatRepository(_repoContext);

        public IGuiEmailRepository _guiEmailRepository;
        public IGuiEmailRepository GuiEmail => _guiEmailRepository ??= new GuiEmailRepository(_repoContext);

        public IAccountRepository _accountRepository;
        public IAccountRepository Account => _accountRepository ??= new AccountRepository(_repoContext);

        #endregion

        //RenderHere
        public async Task SaveAync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
