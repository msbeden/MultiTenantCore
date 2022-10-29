using Microsoft.EntityFrameworkCore;
using Multiple.Models.DatabaseModels.User;

namespace Multiple.Models
{
    public class SharedDbContext : DbContext
    {
        public SharedDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Users> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.1.198,1433;Database=tenant_SharedDB;User Id=sa;Password=Sa123;");
            /*
            var tenantConnectionString = _tenantService.GetConnectionString();
            if (!string.IsNullOrEmpty(tenantConnectionString))
            {
                var dbProvider = _tenantService.GetDatabaseProvider();
                if (dbProvider.ToLower() == "mssql")
                    
            }
            */
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}