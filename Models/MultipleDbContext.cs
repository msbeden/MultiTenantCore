using Microsoft.EntityFrameworkCore;
using Multiple.Models.Abstractions;
using Multiple.Models.DatabaseModels.Product;
using Multiple.Services.Abstractions;

namespace Multiple.Models
{
    public class MultipleDbContext : DbContext
    {
        readonly ITenantService _tenantService;
        string tenantId;
        public MultipleDbContext(DbContextOptions options, ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
            tenantId = _tenantService.GetTenant()?.TenantId;
        }
        public DbSet<Products> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().HasQueryFilter(p => p.TenantId == tenantId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantConnectionString = _tenantService.GetConnectionString();
            if (!string.IsNullOrEmpty(tenantConnectionString))
            {
                var dbProvider = _tenantService.GetDatabaseProvider();
                if (dbProvider.ToLower() == "mssql")
                    optionsBuilder.UseSqlServer(tenantConnectionString);
            }
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = tenantId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}