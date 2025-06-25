using StockManagement.Models;

namespace StockManagement.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product[]> GetAllAsync();
    Product GetById(int id);
    Product CreateProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteById(int id);
}
