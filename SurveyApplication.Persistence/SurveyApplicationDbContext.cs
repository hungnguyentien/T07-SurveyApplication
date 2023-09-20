using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Models;

namespace SurveyApplication.Persistence;

public class SurveyApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string>
{
    public SurveyApplicationDbContext(DbContextOptions<SurveyApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SurveyApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
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

    public DbSet<Module> Module { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<ApplicationUser> Account { get; set; }
    public DbSet<LoaiHinhDonVi> LoaiHinhDonVi { get; set; }
    public DbSet<LinhVucHoatDong> LinhVucHoatDong { get; set; }
    public DbSet<GuiEmail> GuiEmail { get; set; }
    public DbSet<DotKhaoSat> DotKhaoSat { get; set; }
    public DbSet<BangKhaoSat> BangKhaoSat { get; set; }
    public DbSet<NguoiDaiDien> NguoiDaiDien { get; set; }
    public DbSet<DonVi> DonVi { get; set; }
    public DbSet<BangKhaoSatCauHoi> BangKhaoSatCauHoi { get; set; }
    public DbSet<XaPhuong> XaPhuong { get; set; }
    public DbSet<QuanHuyen> QuanHuyen { get; set; }
    public DbSet<TinhTp> TinhTp { get; set; }

    #region Câu hỏi

    public DbSet<CauHoi> CauHoi { get; set; }
    public DbSet<Cot> Cot { get; set; }
    public DbSet<Hang> Hang { get; set; }
    public DbSet<KetQua> KetQua { get; set; }
    public DbSet<BaoCaoCauHoi> BaoCaoCauHoi { get; set; }

    #endregion

    public DbSet<JobSchedule> JobSchedule { get; set; }
}