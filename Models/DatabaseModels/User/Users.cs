using Multiple.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multiple.Models.DatabaseModels.User
{
    public class Users : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Users() { }
        public Users(string nameSurname, string userName, string password, string connectionString, string tenantId)
        {
            NameSurname = nameSurname;
            UserName = userName;
            Password = password;
            ConnectionString = connectionString;
            TenantId = tenantId;
        }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
        public string TenantId { get; set; }

    }
}
