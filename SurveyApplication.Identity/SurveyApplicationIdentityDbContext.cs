using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Identity.Configurations;
using SurveyApplication.Identity.Models;

namespace SurveyApplication.Identity
{
    public class SurveyApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SurveyApplicationIdentityDbContext(DbContextOptions<SurveyApplicationIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
