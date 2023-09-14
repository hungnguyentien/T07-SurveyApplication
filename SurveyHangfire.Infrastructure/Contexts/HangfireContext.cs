using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Domain.Models.Abstractions;
using Hangfire.Domain.Models.Hangfire;
using Microsoft.EntityFrameworkCore;

namespace Hangfire.Infrastructure.Contexts
{
    public class HangfireContext : DbContext
    {
        public HangfireContext(DbContextOptions<HangfireContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HangfireContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries<IAuditable>())
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreateDate = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedDate = DateTime.Now;
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
            }
            catch
            {
                // ignored
            }

            return await base.SaveChangesAsync(true, cancellationToken);
        }

        public DbSet<JobSchedule> JobSchedule { get; set; }
    }
}