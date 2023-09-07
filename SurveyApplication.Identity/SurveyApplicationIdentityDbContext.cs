using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyApplication.Domain.Models;
using SurveyApplication.Identity.Configurations;

namespace SurveyApplication.Identity
{
    //public class SurveyApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    //{
    //    public SurveyApplicationIdentityDbContext(DbContextOptions<SurveyApplicationIdentityDbContext> options)
    //        : base(options)
    //    {
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    //        modelBuilder.ApplyConfiguration(new UserConfiguration());
    //        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
    //    }
    //}
}
