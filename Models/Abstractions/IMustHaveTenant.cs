namespace Multiple.Models.Abstractions
{
    public interface IMustHaveTenant
    {
        string TenantId { get; set; }
    }
}
