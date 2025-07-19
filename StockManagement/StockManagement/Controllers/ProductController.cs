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
        var products = await _productService.GetAsync();
        var viewModel = Mapper(products);

        return viewModel;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductViewModel>> GetAsync(int id)
    {
        var product = await _productService.GetAsync(id);

        if (product != null)
        {
            return Ok(product);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ProductViewModel>> PostAsync([FromBody] CreateProductModel model)
    {
        try
        {
            var domainModel = Mapper(model);
            var createdModel = await _productService.CreateAsync(domainModel);
            var viewModel = Mapper(createdModel);

            return Ok(viewModel);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest("Geen juiste waarde meegegeven");
        }
        catch (Exception ex)
        {
            return BadRequest("Server fout");
        }

    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateProductModel model)
    {
        var domainModel = Mapper(model);
        await _productService.UpdateAsync(domainModel);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _productService.DeleteAsync(id);
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