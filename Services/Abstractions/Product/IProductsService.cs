using Multiple.Models.DatabaseModels.Product;

namespace Multiple.Services.Abstractions.Product
{
    public interface IProductsService
    {
        Task<Products> CreateAsync(string name, string description, int rate);
        Task<Products> GetByIdAsync(int id);
        Task<IReadOnlyList<Products>> GetAllAsnyc();
    }
}