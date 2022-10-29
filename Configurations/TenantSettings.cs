using Multiple.Models;

namespace Multiple.Configurations
{
    public class TenantSettings
    {
        public DefaultSettings Defaults { get; set; }
        public List<Tenant> Tenants { get; set; }
    }
}
