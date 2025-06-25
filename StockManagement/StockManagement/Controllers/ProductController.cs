using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.DataContracts;
using StockManagement.Models;
using StockManagement.Interfaces.Services;

namespace StockManagement.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ProductViewModel[]>> GetAsync()
    {
        var products = await _productService.GetAllAsync();
        var viewModel = Mapper(products);

        return viewModel;
    }

    [HttpGet("{id}")]
    public ActionResult<ProductViewModel> Get(int id)
    {
        var product = _productService.GetProductById(id);
        
        if (product != null)
        {
            return Ok(product);
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<ProductViewModel> Post([FromBody] CreateProductModel model)
    {
        var domainModel = Mapper(model);
        var createdModel = _productService.Create(domainModel);
        var viewModel = Mapper(createdModel);

        return Ok(viewModel);
    }

    [HttpPut]
    public ActionResult Update([FromBody] UpdateProductModel model)
    {
        var domainModel = Mapper(model);
        _productService.Update(domainModel);

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _productService.Delete(id);
        return Ok();
    }

    private ProductViewModel[] Mapper(Product[] model)
    {
        List<ProductViewModel> products = new();
        foreach (var product in model)
        {
            var mappedObject = Mapper(product);
            products.Add(mappedObject);
        }
        return products.ToArray();
    }

    private ProductViewModel Mapper(Product model)
    {
        return new ProductViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            DiscountPercentage = model.DiscountPercentage
        };
    }

    private Product Mapper(CreateProductModel model) 
    {
        return new Product()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            DiscountPercentage = model.DiscountPercentage
        };
    }

    private Product Mapper(UpdateProductModel model)
    {
        return new Product()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            DiscountPercentage = model.DiscountPercentage
        };
    }
}
