using StockManagement.Models;

namespace StockManagement.Interfaces.Services;

public interface ICustomerService
{
    Task<Customer[]> GetAsync();
    Task<Customer> GetAsync(int id);
    Task<Customer> CreateAsync(Customer customer);
    Task DeleteAsync(int id);
    Task UpdateAsync(Customer customer);
}
