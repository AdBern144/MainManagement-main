using StockManagement.Interfaces.Repositories;
using StockManagement.Interfaces.Services;
using StockManagement.Models;

namespace StockManagement.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserService _userService;

    public CustomerService(ICustomerRepository customerRepository, IUserService userService)
    {
        _customerRepository = customerRepository;
        _userService = userService;
    }

    public async Task<Customer[]> GetAsync()
    {
        return await _customerRepository.GetAsync();
    }

    public async Task<Customer> GetAsync(int id)
    {
        return await _customerRepository.GetAsync(id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        var userIdFromToken = _userService.GetCurrentUser().Id;
        customer.UserId = userIdFromToken;

        var createdCustomer = await _customerRepository.CreateAsync(customer);
        return createdCustomer;
    }

    public async Task DeleteAsync(int id)
    {
        await _customerRepository.DeleteAsync(id);
    }

    public async Task UpdateAsync(Customer customer)
    {
        var dbModel = await _customerRepository.GetAsync(customer.Id);

        dbModel.Name = customer.Name;
        dbModel.Email = customer.Email;
        dbModel.Phone = customer.Phone;
        dbModel.Address = customer.Address;
        dbModel.PostalCode = customer.PostalCode;
        dbModel.City = customer.City;
        dbModel.Country = customer.Country;



        await _customerRepository.UpdateAsync(dbModel);
    }
}
