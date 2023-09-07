using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common;

namespace SurveyApplication.Persistence;

public class SurveyApplicationDbContext : DbContext
{
    public SurveyApplicationDbContext(DbContextOptions<SurveyApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SurveyApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.ActiveFlag = 1;
                    break;
                case EntityState.Modified:
                    entry.Entity.Modified = DateTime.Now;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        return base.SaveChangesAsync(cancellationToken);
    }

    #region Dbset

    public DbSet<LoaiHinhDonVi> LoaiHinhDonVi { get; set; }
    public DbSet<LinhVucHoatDong> LinhVucHoatDong { get; set; }
    public DbSet<GuiEmail> GuiEmail { get; set; }
    public DbSet<DotKhaoSat> DotKhaoSat { get; set; }
    public DbSet<BangKhaoSat> BangKhaoSat { get; set; }
    public DbSet<NguoiDaiDien> NguoiDaiDien { get; set; }
    public DbSet<DonVi> DonVi { get; set; }
    public DbSet<BangKhaoSatCauHoi> BangKhaoSatCauHoi { get; set; }

    #region Câu hỏi

    public DbSet<CauHoi> CauHoi { get; set; }
    public DbSet<Cot> Cot { get; set; }
    public DbSet<Hang> Hang { get; set; }
    public DbSet<KetQua> KetQua { get; set; }

    #endregion

    #endregion

}