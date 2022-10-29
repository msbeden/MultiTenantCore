using Microsoft.AspNetCore.Mvc;
using Multiple.Models.ViewModels.Product;
using Multiple.Services.Abstractions.Product;

namespace Multiple.Controllers.ProductControllers
{
    [ApiExplorerSettings(GroupName = "ProductControllers")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        readonly IProductsService _productService;
        public ProductController(IProductsService ProductService)
        {
            _productService = ProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
            => Ok(await _productService.GetAllAsnyc());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
            => Ok(await _productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductsViewModel ProductsViewModel)
            => Ok(await _productService.CreateAsync(ProductsViewModel.Name, ProductsViewModel.Description, ProductsViewModel.Rate));

    }
}