using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Persistence
{
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
            {
                entry.Entity.Modified = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<LoaiHinhDonVi> LoaiHinhDonVi { get; set; }
        public DbSet<LinhVucHoatDong> LinhVucHoatDong { get; set; }
        public DbSet<GuiEmail> GuiEmail { get; set; }
        public DbSet<DotKhaoSat> DotKhaoSat { get; set; }
        public DbSet<BangKhaoSat> BangKhaoSat { get; set; }
        public DbSet<NguoiDaiDien> NguoiDaiDien { get; set; }
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<NguoiDungVaiTro> NguoiDungVaiTro { get; set; }
        public DbSet<Quyen> Quyen { get; set; }
        public DbSet<VaiTro> VaiTro { get; set; }
        public DbSet<VaiTroQuyen> VaiTroQuyen { get; set; }
        public DbSet<DonVi> DonVi { get; set; }

        #region Câu hỏi

        public DbSet<CauHoi> CauHoi { get; set; }
        public DbSet<Cot> Cot { get; set; }
        public DbSet<Hang> Hang { get; set; }
        public DbSet<KetQua> KetQua { get; set; }

        #endregion

    }
}
