using BaseCleanArchitecture.Domain.Abstractions;
using BaseCleanArchitecture.Domain.Employees;
using BaseCleanArchitecture.Infrastructure.EntityConfigurations;
using CD.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCleanArchitecture.Infrastructure.Context
{
    internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntityConfiguration<>).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entities)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property(i => i.CreatedDate).CurrentValue = DateTimeOffset.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    if(entry.Property(i =>i.IsDelete).CurrentValue == true)
                    {
                        entry.Property(i => i.DeleteAt).CurrentValue = DateTimeOffset.Now;
                    }
                    else
                    {
                        entry.Property(i => i.UpdatedDate).CurrentValue = DateTimeOffset.Now;
                    }   
                }

                if(entry.State == EntityState.Deleted)
                {
                    throw new ArgumentException("You can not delete this entity. You can only set IsDelete property to true.");
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
