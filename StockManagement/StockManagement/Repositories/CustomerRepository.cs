using Microsoft.EntityFrameworkCore;
using StockManagement.Interfaces.Repositories;
using StockManagement.Interfaces.Services;
using StockManagement.Models;

namespace StockManagement.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;

    public CustomerRepository(AppDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    public async Task<Customer[]> GetAsync()
    {
        var userId = _userService.GetCurrentUser().Id;
        return await _context.Customers.Where(x => x.UserId == userId).ToArrayAsync();
    }

    public async Task<Customer> GetAsync(int id)
    {
        return await _context.Customers.SingleAsync(x => x.Id == id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var toBeDeleted = await GetAsync(id);
        _context.Customers.Remove(toBeDeleted);
        await _context.SaveChangesAsync();
    }
}