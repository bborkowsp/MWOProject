using SharedService;
using SharedService.Shop;

namespace SharedService.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();

        Task<ServiceResponse<Product>> UpdateProductAsync(Product product);

        Task<ServiceResponse<bool>> DeleteProductAsync(int id);

        Task<ServiceResponse<Product>> CreateProductAsync(Product product);

        Task<ServiceResponse<Product>> GetProductByIdAsync(int id);
    }
}
