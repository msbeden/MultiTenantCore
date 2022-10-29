using Microsoft.EntityFrameworkCore;
using Multiple.Models;
using Multiple.Models.DatabaseModels.Product;
using Multiple.Services.Abstractions.Product;

namespace Multiple.Services.Product
{
    public class ProductService : IProductsService
    {
        readonly MultipleDbContext _context;

        public ProductService(MultipleDbContext context)
        {
            _context = context;
        }

        public async Task<Products> CreateAsync(string name, string description, int rate)
        {
            Products product = new(name, description, rate);
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IReadOnlyList<Products>> GetAllAsnyc()
            => await _context.Product.ToListAsync();

        public async Task<Products> GetByIdAsync(int id)
            => await _context.Product.FindAsync(id);
    }

}
