using StockManagement.Interfaces.Repositories;
using StockManagement.Interfaces.Services;
using StockManagement.Models;

namespace StockManagement.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product[]> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public Product GetProductById(int id)
    {
        return _repository.GetById(id);
    }

    public Product Create(Product product)
    {
        var createdProduct = _repository.CreateProduct(product);
        return createdProduct;
    }

    public void Delete(int id)
    {
        _repository.DeleteById(id);
    }

    public void Update(Product product)
    {
        var dbModel = _repository.GetById(product.Id);

        dbModel.Name = product.Name;
        dbModel.Description = product.Description;
        dbModel.Price = product.Price;
        dbModel.DiscountPercentage = product.DiscountPercentage;
        dbModel.Stock = product.Stock;

        _repository.UpdateProduct(dbModel);
    }
}
