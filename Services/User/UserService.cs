using Microsoft.EntityFrameworkCore;
using Multiple.Models;
using Multiple.Models.DatabaseModels.User;
using Multiple.Services.Abstractions.User;

namespace Multiple.Services.User
{

    public class UserService : IUsersService
    {
        readonly SharedDbContext _context;

        public UserService(SharedDbContext context)
        {
            _context = context;
        }

        public async Task<Users> CreateAsync(string nameSurname, string userName, string password, string connectionString, string tenantId)
        {
            Users user = new(nameSurname, userName, password, connectionString, tenantId);
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IReadOnlyList<Users>> GetAllAsnyc()
            => await _context.User.ToListAsync();

        public async Task<Users> GetByIdAsync(int id)
            => await _context.User.FindAsync(id);
    }
}
