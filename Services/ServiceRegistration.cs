using Microsoft.EntityFrameworkCore;
using Multiple.Configurations;
using Multiple.Models;
using Multiple.Services.Abstractions;
using Multiple.Services.Abstractions.Product;
using Multiple.Services.Abstractions.User;
using Multiple.Services.Product;
using Multiple.Services.User;

namespace Multiple.Services
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection collection)
        {
            collection.AddTransient<ITenantService, TenantService>();
        }

        public static async Task AddPersistenceService(this IServiceCollection collection)
        {
            collection.AddScoped<IUsersService, UserService>();
            collection.AddScoped<IProductsService, ProductService>();

            using var provider = collection.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            var tenantSettings = configuration.GetSection("TenantSettings").Get<TenantSettings>();

            var defaultConnectionString = tenantSettings.Defaults?.ConnectionString;
            var defaultDbProvider = tenantSettings.Defaults?.DbProvider;

            if (defaultDbProvider.ToLower() == "mssql")
            {
                collection.AddDbContext<SharedDbContext>(option => option.UseSqlServer(e => e.MigrationsAssembly(typeof(SharedDbContext).Assembly.FullName)));
            }

            using IServiceScope scope = collection.BuildServiceProvider().CreateScope();
            // SharedDB Migration
            var dbContext = scope.ServiceProvider.GetRequiredService<SharedDbContext>();
            dbContext.Database.SetConnectionString(defaultConnectionString);
            if (dbContext.Database.GetMigrations().Count() > 0)
                await dbContext.Database.MigrateAsync();

            if (defaultDbProvider.ToLower() == "mssql")
            {
                collection.AddDbContext<MultipleDbContext>(option => option.UseSqlServer(e => e.MigrationsAssembly(typeof(MultipleDbContext).Assembly.FullName)));
            }

            using IServiceScope scope2 = collection.BuildServiceProvider().CreateScope();
            foreach (var tenant in tenantSettings.Tenants)
            {
                string connectionString = tenant.ConnectionString switch
                {
                    null => defaultConnectionString,
                    not null => tenant.ConnectionString
                };
                var dbContext2 = scope2.ServiceProvider.GetRequiredService<MultipleDbContext>();
                dbContext2.Database.SetConnectionString(connectionString);
                if (dbContext2.Database.GetMigrations().Count() > 0)
                    await dbContext2.Database.MigrateAsync();
            }
        }
    }
}