using Multiple.Models.DatabaseModels.User;

namespace Multiple.Services.Abstractions.User
{
    public interface IUsersService
    {
        Task<Users> CreateAsync(string nameSurname, string userName, string password, string connectionString, string tenantId);
        Task<Users> GetByIdAsync(int id);
        Task<IReadOnlyList<Users>> GetAllAsnyc();
    }
}