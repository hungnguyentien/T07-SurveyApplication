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

        public DbSet<LoaiHinhDonVi> LoaiHinhDonVis { get; set; }
        public DbSet<LoaiCauHoi> LoaiCauHoiVis { get; set; }
        public DbSet<LinhVucHoatDong>LinhVucHoatDongs { get; set; }
        public DbSet<GuiEmail>GuiEmails { get; set; }
        public DbSet<DotKhaoSat> DotKhaoSats { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<BangKhaoSat> BangKhaoSats { get;set; }
        public DbSet<NguoiDaiDien>NguoiDaiDiens { get; set; }
        public DbSet<NguoiDung>NguoiDungs { get; set; }
        public DbSet<NguoiDungVaiTro> NguoiDungVaiTros { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<VaiTroQuyen> VaiTroQuyens { get; set; }
        public DbSet<DonVi>DonVis { get;set; }
    }
}
