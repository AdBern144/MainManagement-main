using Microsoft.EntityFrameworkCore;
using StockManagement.Interfaces.Repositories;
using StockManagement.Interfaces.Services;
using StockManagement.Models;

namespace StockManagement.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;

    public ProductRepository(AppDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    public async Task<Product[]> GetAsync()
    {
        var userId = _userService.GetCurrentUser().Id;
        return await _context.Products.Where(x => x.UserId == userId).ToArrayAsync();
    }

    public async Task<Product> GetAsync(int id)
    {
        return await _context.Products.SingleAsync(x => x.Id == id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var toBeDeleted = await GetAsync(id);
        _context.Products.Remove(toBeDeleted);
        await _context.SaveChangesAsync();
    }
}