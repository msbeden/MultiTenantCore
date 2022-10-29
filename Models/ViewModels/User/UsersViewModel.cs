using Multiple.Models.Abstractions;

namespace Multiple.Models.ViewModels.User
{
    public class UsersViewModel : IViewModel
    {
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
        public string TenantId { get; set; }
        public short? Status { get; set; }
    }
}