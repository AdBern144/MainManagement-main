using StockManagement.Models;

namespace StockManagement.Interfaces.Repositories;

public interface IUserRepository
{
    User? Get(string email);
    User? Get(int id);
    Task<User> CreateAsync(User user);
    void Update(User user);
    void Delete(int id);
}