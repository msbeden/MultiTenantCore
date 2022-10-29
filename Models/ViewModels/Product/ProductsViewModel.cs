using Multiple.Models.Abstractions;

namespace Multiple.Models.ViewModels.Product
{
    public class ProductsViewModel : IViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public short? Status { get; set; }
    }
}