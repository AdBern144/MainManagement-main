using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.DataContracts;
using StockManagement.Models;
using StockManagement.Interfaces.Services;

namespace StockManagement.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerViewModel[]>> GetAsync()
    {
        var customers = await _customerService.GetAsync();
        var viewModel = Mapper(customers);

        return viewModel;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerViewModel>> GetAsync(int id)
    {
        var customer = await _customerService.GetAsync(id);

        if (customer != null)
        {
            return Ok(customer);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<CustomerViewModel>> PostAsync([FromBody] CreateCustomerModel model)
    {
        try
        {
            var domainModel = Mapper(model);
            var createdModel = await _customerService.CreateAsync(domainModel);
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
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateCustomerModel model)
    {
        var domainModel = Mapper(model);
        await _customerService.UpdateAsync(domainModel);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _customerService.DeleteAsync(id);
        return Ok();
    }

    private CustomerViewModel[] Mapper(Customer[] model)
    {
        List<CustomerViewModel> customers = new();
        foreach (var customer in model)
        {
            var mappedObject = Mapper(customer);
            customers.Add(mappedObject);
        }
        return customers.ToArray();
    }

    private CustomerViewModel Mapper(Customer model)
    {
        return new CustomerViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Address = model.Address,
            PostalCode = model.PostalCode,
            City = model.City,
            Country = model.Country
        };
    }

    private Customer Mapper(CreateCustomerModel model)
    {
        return new Customer()
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Address = model.Address,
            PostalCode = model.PostalCode,
            City = model.City,
            Country = model.Country
        };
    }

    private Customer Mapper(UpdateCustomerModel model)
    {
        return new Customer()
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Address = model.Address,
            PostalCode = model.PostalCode,
            City = model.City,
            Country = model.Country
        };
    }
}