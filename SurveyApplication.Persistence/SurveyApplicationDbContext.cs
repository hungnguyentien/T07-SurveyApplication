using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Persistence
{
    public class SurveyApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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
            {
                entry.Entity.Modified = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.ActiveFlag = 1;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        //public DbSet<ApiResourceProperty> ApiResourceProperty { get; set; }
        //public DbSet<IdentityResourceProperty> IdentityResourceProperty { get; set; }
        //public DbSet<ApiResourceSecret> ApiResourceSecret { get; set; }
        //public DbSet<ApiScopeClaim> ApiScopeClaim { get; set; }
        //public DbSet<IdentityResourceClaim> IdentityResourceClaim { get; set; }
        //public DbSet<ApiResourceClaim> ApiResourceClaim { get; set; }
        //public DbSet<ClientGrantType> ClientGrantType { get; set; }
        //public DbSet<ClientScope> ClientScope { get; set; }
        //public DbSet<ClientSecret> ClientSecret { get; set; }
        //public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUri { get; set; }
        //public DbSet<ClientIdPRestriction> ClientIdPRestriction { get; set; }
        //public DbSet<ClientRedirectUri> ClientRedirectUri { get; set; }
        //public DbSet<ClientClaim> ClientClaim { get; set; }
        //public DbSet<ClientProperty> ClientProperty { get; set; }
        //public DbSet<ApiScopeProperty> ApiScopeProperty { get; set; }
        //public DbSet<ApiResourceScope> ApiResourceScope { get; set; }

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

        #endregion

    }
}
