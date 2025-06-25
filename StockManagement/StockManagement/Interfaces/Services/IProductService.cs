using StockManagement.Models;

namespace StockManagement.Interfaces.Services;

public interface IProductService
{
    Task<Product[]> GetAllAsync();
    Product GetProductById(int id);
    Product Create(Product product);
    void Delete(int id);
    void Update(Product product);
}
