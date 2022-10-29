using Multiple.Models;

namespace Multiple.Services.Abstractions
{
    public interface ITenantService
    {
        string GetDatabaseProvider();
        string GetConnectionString();
        Tenant GetTenant();
    }
}
